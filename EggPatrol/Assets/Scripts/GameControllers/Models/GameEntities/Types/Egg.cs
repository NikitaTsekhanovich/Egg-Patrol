using System;
using DG.Tweening;
using GameControllers.Bootstrap.Properties;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Configs;
using GameControllers.Models.GameEntities.Properties;
using UnityEngine;
using Zenject;

namespace GameControllers.Models.GameEntities.Types
{
    public class Egg : Entity, IHaveUpdate
    {
        [SerializeField] private EggConfig _eggConfig;
        [SerializeField] private ScoreIncreaserCollisionHandler _scoreIncreaserCollisionHandler;
        [SerializeField] private InsideEggCollisionHandler _insideEggCollisionHandler;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private GameObject _eggModel;
        [SerializeField] private GameObject _brokeEggModel;
        [SerializeField] private AudioSource _brokeEggSound;
        
        private const float DisappearTime = 4f;

        private EggAnimatorController _eggAnimatorController;
        private IGameStateController _gameStateController;
        private float _speed;
        private int _scoreValue;
        private bool _isBroke;
        private float _currentLifeTime;

        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;

            _gameStateController.OnPauseGame += PauseAnimator;
        }

        public override void SpawnInit(Action<IEntity> returnAction)
        {
            base.SpawnInit(returnAction);
            
            _speed = _eggConfig.Speed;
            _scoreValue = _eggConfig.ScoreValue;

            _eggAnimatorController = new EggAnimatorController(GetComponent<Animator>());
            
            _scoreIncreaserCollisionHandler.CollisionScoreIncreaser += CollideScoreIncreaser;
            _insideEggCollisionHandler.RemoveInsideEgg += CollideInteractingInsideEgg;
            _groundChecker.StateBeingGround += Break;
        }

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            
            _brokeEggModel.SetActive(false);
            _eggAnimatorController.Break(false);
            _eggModel.SetActive(true);
            _isBroke = false;
            _currentLifeTime = 0f;
        }

        public void UpdateSystem()
        {
            if (!IsActive) return;
            
            if (_isBroke)
            {
                CheckLifeTime();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            transform.position += Vector3.down * Time.deltaTime * _speed;
        }

        private void OnDestroy()
        {
            _gameStateController.OnPauseGame -= PauseAnimator;
            _scoreIncreaserCollisionHandler.CollisionScoreIncreaser -= CollideScoreIncreaser;
            _insideEggCollisionHandler.RemoveInsideEgg -= CollideInteractingInsideEgg;
            _groundChecker.StateBeingGround -= Break;
        }

        private void Break(bool isGround)
        {
            if (isGround)
            {
                _brokeEggSound.Play();
                _isBroke = true;
                _eggModel.SetActive(false);
                _brokeEggModel.SetActive(true);
                _eggAnimatorController.Break(true);
            }
        }

        private void CheckLifeTime()
        {
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime >= DisappearTime)
            {
                _currentLifeTime = 0f;
                ReturnToPool();
            }
        }

        private void CollideScoreIncreaser(ICanIncreaseScore scoreIncreaser)
        {
            scoreIncreaser.IncreaseScore(_scoreValue);
            ReturnToPool();
        }

        private void CollideInteractingInsideEgg()
        {
            _currentLifeTime = 0f;
            ReturnToPool();
        }

        private void PauseAnimator(bool state)
        {
            _eggAnimatorController.PauseAnimator(state);
        }
    }
}
