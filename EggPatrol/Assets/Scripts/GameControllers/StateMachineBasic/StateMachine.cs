using System;
using System.Collections.Generic;
using GameControllers.Controllers.Properties;

namespace GameControllers.StateMachineBasic
{
    public class StateMachine : IHaveUpdate, IHaveFixedUpdate
    {
        private IHaveUpdate _updateState;
        private IHaveFixedUpdate _fixedUpdateState;
        private IState _currentState;
        
        protected Dictionary<Type, IState> States;
        
        public void EnterIn<TState>() 
            where TState : IState
        {
            if (States.TryGetValue(typeof(TState), out IState state))
            {
                _currentState?.Exit();
                
                _currentState = state;
                _updateState = _currentState as IHaveUpdate;
                _fixedUpdateState = _currentState as IHaveFixedUpdate;
                
                _currentState.Enter();
            }
        }

        public Type GetStateType()
        {
            return _currentState?.GetType();
        }

        public void UpdateSystem()
        {
            _updateState?.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            _fixedUpdateState?.FixedUpdateSystem();
        }
    }
}
