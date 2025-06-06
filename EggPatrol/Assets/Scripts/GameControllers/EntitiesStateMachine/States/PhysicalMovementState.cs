using System;
using DG.Tweening;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine.States
{
    public abstract class PhysicalMovementState : IState, IHaveFixedUpdate, IDisposable
    {
        private readonly PlayerAnimatorController _playerAnimatorController;
        private readonly float _speed;
        private readonly float _maxVelocity;
        private readonly Rigidbody _rigidbody;
        private readonly GroundChecker _groundChecker;
        private readonly AudioSource _runSound;
        
        protected readonly Transform Transform;
        
        protected Vector3 Direction;

        private bool _isGrounded;

        protected PhysicalMovementState(
            Rigidbody rigidbody, 
            float speed,
            float maxVelocity,
            PlayerAnimatorController playerAnimatorController,
            Transform transform,
            GroundChecker groundChecker,
            AudioSource runSound)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            _maxVelocity = maxVelocity;
            _playerAnimatorController = playerAnimatorController;
            Transform = transform;
            _groundChecker = groundChecker;
            _runSound = runSound;

            _groundChecker.StateBeingGround += CheckGround;
        }
        
        public void Enter()
        {
            SetDirection();
            _playerAnimatorController.Run(true);
        }

        public void FixedUpdateSystem()
        {
            Move();
        }

        public void Exit()
        {
            StopMove();
            _runSound.Stop();
            _playerAnimatorController.Run(false);
            Transform.DOKill();
        }
        
        public void Dispose()
        {
            _groundChecker.StateBeingGround -= CheckGround;
        }

        protected abstract void SetDirection();

        private void Move()
        {
            PlayRunSound(); 
            
            var currentVelocity = _rigidbody.linearVelocity;
            currentVelocity.x += Direction.x * Time.deltaTime * _speed;
            var velocityX = Mathf.Clamp(currentVelocity.x, -_maxVelocity, _maxVelocity);
            
            _rigidbody.linearVelocity = new Vector3(velocityX, currentVelocity.y, 0f);
        }

        private void PlayRunSound()
        {
            if (_isGrounded && !_runSound.isPlaying)
                _runSound.Play();
            else if (!_isGrounded && _runSound.isPlaying)
                _runSound.Stop();
        }
        
        private void CheckGround(bool isGrounded) => _isGrounded = isGrounded;

        private void StopMove()
        {
            _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
        }
    }
}
