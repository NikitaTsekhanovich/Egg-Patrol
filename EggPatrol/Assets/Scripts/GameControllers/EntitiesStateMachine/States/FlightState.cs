using System;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine.States
{
    public class FlightState : IState, IDisposable, IHaveUpdate
    {
        private readonly GroundChecker _groundChecker;
        private readonly float _jumpForce;
        private readonly float _linearDamping;
        private readonly Rigidbody _rigidbody;
        private readonly PlayerAnimatorController _playerAnimatorController;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly AudioSource _jumpSound;
        
        private bool _isGrounded;
        
        public FlightState(
            GroundChecker groundChecker, 
            float jumpForce,
            float linearDamping,
            Rigidbody rigidbody,
            PlayerAnimatorController playerAnimatorController,
            PlayerStateMachine playerStateMachine,
            AudioSource jumpSound)
        {
            _groundChecker = groundChecker;
            _jumpForce = jumpForce;
            _linearDamping = linearDamping;
            _rigidbody = rigidbody;
            _playerAnimatorController = playerAnimatorController;
            _playerStateMachine = playerStateMachine;
            _jumpSound = jumpSound;
            
            _groundChecker.StateBeingGround += CheckStateBeingGround;
        }
        
        public void Enter()
        {
            CanJump();
        }

        public void UpdateSystem()
        {
            if (_rigidbody.linearVelocity.y < 0)
                _rigidbody.linearDamping = _linearDamping;
        }

        public void Exit()
        {
            _rigidbody.linearDamping = 0;
        }
        
        public void Dispose()
        {
            _groundChecker.StateBeingGround -= CheckStateBeingGround;
        }
        
        private void CanJump()
        {
            if (_isGrounded)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _jumpSound.Play();
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        
        private void CheckStateBeingGround(bool isGrounded)
        {
            _isGrounded = isGrounded;

            if (_playerStateMachine.GetStateType() == typeof(StunState))
            {
                _playerAnimatorController.Jump(false);
                return;
            }
            
            _playerAnimatorController.Jump(!_isGrounded);

            if (_isGrounded && 
                !(_playerStateMachine.GetStateType() == typeof(LeftMovementState) ||
                  _playerStateMachine.GetStateType() == typeof(RightMovementState) || 
                  _playerStateMachine.GetStateType() == typeof(SlipState)))
            {
                _playerStateMachine.EnterIn<IdleState>();
            }
        }
    }
}
