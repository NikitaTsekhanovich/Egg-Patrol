using System.Collections.Generic;
using DG.Tweening;
using GameControllers.Bootstrap.Properties;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.EntitiesStateMachine;
using GameControllers.EntitiesStateMachine.States;
using GameControllers.Models.Configs;
using GameControllers.Models.Enums;
using GameControllers.Views;
using SaveSystems;
using UnityEngine;
using Zenject;

namespace GameControllers.Models
{
    public class Player : MonoBehaviour, IPlayer
    {
        [Header("Components")]
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private ParticleSystem _bleedParticleEffect;
        [SerializeField] private ParticleSystem _combustionEffect;
        [SerializeField] private ParticleSystem _immunityEffect;
        [SerializeField] private GameObject _chickenModel;
        [SerializeField] private GameObject _roastedChickenModel;
        [Header("Sounds")] 
        [SerializeField] private AudioSource _hitDamageSound;
        [SerializeField] private AudioSource _jumpSound;
        [SerializeField] private AudioSource _periodicDamageSound;
        [SerializeField] private AudioSource _slipSound;
        [SerializeField] private AudioSource _resurrectSound;
        [SerializeField] private AudioSource _roastedChickenSound;
        [SerializeField] private AudioSource _increaseScoreSound;
        [SerializeField] private AudioSource _runSound;
        
        [Inject] private SaveSystem _saveSystem;
        [Inject] private IGameStateController _gameStateController;
        
        private readonly Vector3 _scaleRoastedChicken = new (20f, 20f, 20f);
        private readonly List<ICanRequestSave> _preservers = new ();
        
        private const float BorderOnEarth = 0.82f;
        
        private HealthHandler _healthHandler;
        private EggsScoreHandler _eggsScoreHandler;
        private PlayerAnimatorController _playerAnimatorController;
        private IInputController _inputController;
        private PlayerStateMachine _playerStateMachine; 
        private ImmunityAbilityController _immunityAbilityController;
        private Rigidbody _rigidbody;
        private bool _isStunned;
        private bool _isDead;
        private bool _hasImmunity;
        private bool _hasInvulnerable;
        
        public Transform TargetTransform { get; private set; }
        
        public void Init(IInputController inputController, EggsScoreView eggsScoreView, HealthView healthView, bool hasInvulnerable = false)
        {
            _hasInvulnerable = hasInvulnerable;
            TargetTransform = transform;
            
            _rigidbody = GetComponent<Rigidbody>();
            
            _eggsScoreHandler  = new EggsScoreHandler(eggsScoreView, _saveSystem, _increaseScoreSound);
            _playerAnimatorController = new PlayerAnimatorController(GetComponent<Animator>());
            
            _playerStateMachine = new PlayerStateMachine(
                _playerAnimatorController, 
                _rigidbody,
                _playerConfig.Speed,
                _playerConfig.JumpForce,
                _playerConfig.SlipTime, 
                _playerConfig.LinerDamping,
                _playerConfig.SlipForce,
                _playerConfig.MaxVelocity,
                _groundChecker,
                transform, 
                BlockInput,
                _jumpSound,
                _slipSound,
                _runSound);
            
            _healthHandler = new HealthHandler(
                healthView, 
                _playerConfig.MaxHealth, 
                _saveSystem, 
                Die, 
                _combustionEffect, 
                _bleedParticleEffect,
                _playerStateMachine,
                _hitDamageSound,
                _periodicDamageSound,
                _resurrectSound,
                _hasInvulnerable);
            
            _immunityAbilityController = new ImmunityAbilityController(
                UseImmunityAbility,
                _immunityEffect,
                _healthHandler,
                _saveSystem);
            
            _inputController = inputController;

            _inputController.HoldMoveRightButton += HoldMoveRight;
            _inputController.HoldMoveLeftButton += HoldMoveLeft;
            _inputController.StopHoldMoveButton += StopHoldMove;
            _inputController.ClickJumpButton += PressJump;
            _gameStateController.OnPauseGame += PauseAnimator;
            
            _preservers.Add(_eggsScoreHandler);
        }
        
        public void UpdateSystem()
        {
            _healthHandler?.TakePeriodicDamage();
            _playerStateMachine?.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            _playerStateMachine?.FixedUpdateSystem();
        }
        
        private void OnDestroy()
        {
            _inputController.HoldMoveRightButton -= HoldMoveRight;
            _inputController.HoldMoveLeftButton -= HoldMoveLeft;
            _inputController.StopHoldMoveButton -= StopHoldMove;
            _inputController.ClickJumpButton -= PressJump;
            _gameStateController.OnPauseGame -= PauseAnimator;

            _roastedChickenModel.transform.DOKill();
            
            _playerStateMachine?.Dispose();
        }
        
        public void TakeDamage(DamageType damageType, int damage)
        {
            if (_hasImmunity) return;
            
            _healthHandler?.TakeDamage(damageType, damage);
        }
        
        public void TakePeriodicDamage(DamageType damageType, int damage, float timeInterval, float periodicDamageDuration)
        {
            if (_hasImmunity) return;
            
            _healthHandler?.SetPeriodicDamage(damageType, damage, timeInterval, periodicDamageDuration);
        }
        
        public void IncreaseScore(int score)
        {
            _eggsScoreHandler?.IncreaseEggs(score);
        }
        
        public void InteractInsideEgg()
        {
            _playerStateMachine?.EnterIn<SlipState>();
        }
        
        public bool TryReact()
        {
            return _immunityAbilityController.TryUse();
        }

        private void PauseAnimator(bool state)
        {
            _playerAnimatorController.PauseAnimator(state);
        }

        private void UseImmunityAbility(bool state)
        {
            _hasImmunity = state;
        }

        private void HoldMoveRight()
        {
            if (_isStunned) return;
            
            _playerStateMachine?.EnterIn<RightMovementState>();
        }
        
        private void HoldMoveLeft()
        {
            if (_isStunned) return;
            
            _playerStateMachine?.EnterIn<LeftMovementState>();
        }

        private void StopHoldMove()
        {
            if (_isStunned) return;
            
            if (transform.position.y > BorderOnEarth)
                _playerStateMachine?.EnterIn<FlightState>();
            else
                _playerStateMachine?.EnterIn<IdleState>();
        }

        private void PressJump()
        {
            if (_isStunned) return;
            
            _playerStateMachine?.EnterIn<FlightState>();
        }

        private void BlockInput(bool state)
        {
            _isStunned = state;
        }

        private void SetRoastedChickenModel()
        {
            _roastedChickenSound.Play();
            _roastedChickenModel.SetActive(true);
            _chickenModel.SetActive(false);
            _roastedChickenModel.transform.DOScale(_scaleRoastedChicken, 1f);
        }

        private void Die(DamageType damageType)
        {
            _gameStateController.EndGame();
            
            _inputController.HoldMoveRightButton -= HoldMoveRight;
            _inputController.HoldMoveLeftButton -= HoldMoveLeft;
            _inputController.StopHoldMoveButton -= StopHoldMove;
            _inputController.ClickJumpButton -= PressJump;

            foreach (var preserver in _preservers)
                preserver.RequestSave();
            
            _playerStateMachine.EnterIn<DeadState>();
            _playerStateMachine = null;
            _healthHandler = null;
            _eggsScoreHandler = null;
            
            switch (damageType)
            {
                case DamageType.Hit:
                    break;
                case DamageType.Cut:
                    break;
                case DamageType.Combustion:
                    SetRoastedChickenModel();
                    break;
                case DamageType.Electricity:
                    break;
            }
        }
    }
}
