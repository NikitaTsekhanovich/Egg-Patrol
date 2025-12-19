using System;
using System.Collections.Generic;
using GameControllers.Bootstrap.StateMachine.States;
using GameControllers.Controllers;
using GameControllers.Models.DataContainers;
using GameControllers.StateMachineBasic;
using MusicSystem;
using SaveSystems;
using Zenject;

namespace GameControllers.Bootstrap.StateMachine
{
    public class GameStateMachine : StateMachineBasic.StateMachine
    {
        private readonly GameSystemsHandler _gameSystemsHandler;
        
        public GameStateMachine(
            DiContainer container, 
            LoadGameData loadGameData, 
            RunTimeGameData runTimeGameData,
            ScoreHandler scoreHandler,
            SaveSystem saveSystem,
            MusicSwitcher musicSwitcher)
        {
            _gameSystemsHandler = new GameSystemsHandler();
            
            States = new Dictionary<Type, IState>
            {
                [typeof(LoadGameState)] = new LoadGameState(
                    this, 
                    container, 
                    loadGameData, 
                    runTimeGameData, 
                    _gameSystemsHandler,
                    scoreHandler,
                    saveSystem,
                    musicSwitcher),
                [typeof(LoopGameState)] = new LoopGameState(_gameSystemsHandler),
                [typeof(PauseGameState)] = new PauseGameState()
            };
        }
    }
}
