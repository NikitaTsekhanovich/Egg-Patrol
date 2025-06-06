using System;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine.States
{
    public class StunState : IState, IHaveUpdate
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerAnimatorController _playerAnimatorController;
        private readonly Action<bool> _blockInput;

        private float _currentTime;
        
        public StunState(
            PlayerStateMachine playerStateMachine, 
            PlayerAnimatorController playerAnimatorController,
            Action<bool> blockInput)
        {
            _playerStateMachine = playerStateMachine;
            _playerAnimatorController = playerAnimatorController;
            _blockInput = blockInput;
        }
        
        public void Enter()
        {
            _blockInput?.Invoke(true);
            _currentTime = 0f;
            _playerAnimatorController.Jump(false);
            _playerAnimatorController.Stun(true);
        }

        public void UpdateSystem()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= 2f)
            {
                _currentTime = 0f;
                _blockInput?.Invoke(false);
                _playerStateMachine.EnterIn<IdleState>();
            }
        }

        public void Exit()
        {
            _playerAnimatorController.Stun(false);
        }
    }
}
