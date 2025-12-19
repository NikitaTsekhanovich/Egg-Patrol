using System;
using DG.Tweening;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Enums;
using GameControllers.Models.GameEntities.Properties;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameControllers.Models.GameEntities.Types.EdgedWeapons
{
    public class EdgedWeapon : AttackEntity, ICanDisappearWithClick, IHaveUpdate
    {
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _periodicDamage;
        [SerializeField] private int _periodicDamageInterval;
        
        private float _currentLifeTime;
        private float _currentPeriodicDamageTime;
        private bool _isGrounded;
        private bool _isHitDamageTaker;
        private ICanTakeDamage _damageTaker;

        public override void SpawnInit(Action<IEntity> returnAction)
        {
            base.SpawnInit(returnAction);
            _groundChecker.StateBeingGround += CollideGround;
        }

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            _currentLifeTime = 0f;
            _currentPeriodicDamageTime = 0f;
            _isGrounded = false;
            _isHitDamageTaker = false;
            StartRotate();
        }

        public void UpdateSystem()
        {
            if (!gameObject.activeSelf) return;
            
            if (!_isGrounded && !_isHitDamageTaker)
            {
                Fall();
            }
            else
            {
                CheckLifeTime();
            }

            if (_isHitDamageTaker)
            {
                DealPeriodicDamage();
            }
        }

        protected override void OnDestroy()
        {
            _groundChecker.StateBeingGround -= CollideGround;
            transform.DOKill();
            base.OnDestroy();
        }

        private void DealPeriodicDamage()
        {
            _currentPeriodicDamageTime += Time.deltaTime;

            if (_currentPeriodicDamageTime >= _periodicDamageInterval)
            {
                _currentPeriodicDamageTime = 0f;
                _damageTaker.TakeDamage(DamageType.Cut, _periodicDamage);
            }
        }

        private void StartRotate()
        {
            float angle;
            var randomValue = Random.Range(0, 2);
            
            if (randomValue == 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                angle = -360f;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 180f);
                angle = 360f;
            }
            
            transform
                .DORotate(new Vector3(0f, 0f, angle), 2f, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        private void CheckLifeTime()
        {
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime >= _lifeTime)
            {
                ReturnToPool();
                _currentLifeTime = 0;
            }
        }

        private void Fall()
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }

        private void CollideGround(bool isGround)
        {
            if (isGround)
            {
                transform.DOKill();
                _isGrounded = true;
            }
        }

        protected override void ReturnToPool()
        {
            transform.parent = null;
            transform.DOKill();
            base.ReturnToPool();
        }
        
        protected override void CollideDamageTaker(ICanTakeDamage damageTaker)
        {
            _currentLifeTime = 0f;
            transform.DOKill();
            
            _damageTaker = damageTaker;
            transform.SetParent(damageTaker.TargetTransform);
            
            _isHitDamageTaker = true;
            base.CollideDamageTaker(damageTaker);
        }

        public bool TryReact()
        {
            if (_isGrounded || _isHitDamageTaker)
                ReturnToPool();
            
            return _isGrounded || _isHitDamageTaker;
        }

        public void DisappearWithClick()
        {
            ReturnToPool();
        }
    }
}
