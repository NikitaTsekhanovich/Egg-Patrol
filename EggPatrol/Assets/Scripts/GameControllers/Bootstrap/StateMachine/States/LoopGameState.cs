using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;

namespace GameControllers.Bootstrap.StateMachine.States
{
    public class LoopGameState : IState, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly GameSystemsHandler _gameSystemsHandler;
        
        public LoopGameState(GameSystemsHandler gameSystemsHandler)
        {
            _gameSystemsHandler = gameSystemsHandler;
        }
        
        public void Enter()
        {
            
        }

        public void UpdateSystem()
        {
            _gameSystemsHandler?.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            _gameSystemsHandler?.FixedUpdateSystem();
        }

        public void Exit()
        {
            
        }
    }
}
