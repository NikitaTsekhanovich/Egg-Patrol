using DG.Tweening;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Models.GameEntities.Types.Haystacks
{
    public abstract class Haystack : AttackEntity, IHaveUpdate, ICanDisappearWithClick
    {
        [Header("Haystack Properties")]
        [SerializeField] private ParticleSystem _collisionParticleSystem;
        [SerializeField] private float _speed;
        [SerializeField] private float _liveTime;
        [SerializeField] private AudioSource _hitSound;
        
        private float _currentTime;
        private Sequence _rotateSequence;
        private bool _isDead;
        
        protected Vector3 DirectionMove;

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            
            ChooseDirection(startPosition);
            _currentTime = 0f;
            _isDead = false;
        }
        
        protected abstract void ChooseDirection(Vector3 startPosition);

        protected override void ReturnToPool()
        {
            _isDead = true;
            base.ReturnToPool();
        }
        
        protected void StartRotate(float angle)
        {
            transform
                .DORotate(new Vector3(0f, 0f, angle), 2f, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
        
        protected override void CollideDamageTaker(ICanTakeDamage damageTaker)
        {
            base.CollideDamageTaker(damageTaker);
            _collisionParticleSystem.Play();
            _hitSound.Play();
        }

        public void UpdateSystem()
        {
            if (_isDead || !IsActive) return;
            
            transform.position += DirectionMove * Time.deltaTime * _speed;
            _currentTime += Time.deltaTime;
            
            if (_currentTime >= _liveTime)
                ReturnToPool();
        }

        protected override void OnDestroy()
        {
            transform.DOKill();
            base.OnDestroy();
        }

        public bool TryReact()
        {
            return false;
        }

        public void DisappearWithClick()
        {
            ReturnToPool();
        }
    }
}
