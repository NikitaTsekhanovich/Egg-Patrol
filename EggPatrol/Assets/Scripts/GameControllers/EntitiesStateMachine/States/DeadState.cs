using GameControllers.Controllers;
using GameControllers.StateMachineBasic;

namespace GameControllers.EntitiesStateMachine.States
{
    public class DeadState : IState
    {
        private readonly PlayerAnimatorController _playerAnimatorController;
        
        public DeadState(PlayerAnimatorController playerAnimatorController)
        {
            _playerAnimatorController = playerAnimatorController;
        }
        
        public void Enter()
        {
            _playerAnimatorController.DiedFromHit();
        }

        public void Exit()
        {
            
        }
    }
}
