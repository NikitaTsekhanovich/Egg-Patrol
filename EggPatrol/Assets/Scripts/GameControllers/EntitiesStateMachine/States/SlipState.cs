using System;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine.States
{
    public class SlipState : IState, IHaveUpdate
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly float _slipTime;
        private readonly float _slipForce;
        private readonly PlayerAnimatorController _playerAnimatorController;
        private readonly Action<bool> _blockInput;
        private readonly Rigidbody _rigidbody;
        private readonly AudioSource _slipSound;

        private float _currentTime;
        
        public SlipState(
            PlayerStateMachine playerStateMachine,
            float slipTime, 
            float slipForce,
            PlayerAnimatorController playerAnimatorController,
            Action<bool> blockInput,
            Rigidbody rigidbody,
            AudioSource slipSound)
        {
            _playerStateMachine = playerStateMachine;
            _slipTime = slipTime;
            _slipForce = slipForce;
            _playerAnimatorController = playerAnimatorController;
            _blockInput = blockInput;
            _rigidbody = rigidbody;
            _slipSound = slipSound;
        }
        
        public void Enter()
        {
            _blockInput?.Invoke(true);
            _currentTime = 0f;
            _playerAnimatorController.Slip(true);
            
            _slipSound.Play();
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _slipForce, ForceMode.Impulse);
        }

        public void UpdateSystem()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _slipTime)
            {
                _currentTime = 0f;
                _blockInput?.Invoke(false);
                _playerStateMachine.EnterIn<IdleState>();
            }
        }

        public void Exit()
        {
            _playerAnimatorController.Slip(false);
        }
    }
}
