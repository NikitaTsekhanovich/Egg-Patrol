using System;
using DG.Tweening;
using GameControllers.Bootstrap.Properties;
using GameControllers.Bootstrap.StateMachine;
using GameControllers.Bootstrap.StateMachine.States;
using GameControllers.Controllers;
using GameControllers.Models.DataContainers;
using GameControllers.Views;
using MusicSystem;
using SaveSystems;
using UnityEngine;
using Zenject;

namespace GameControllers.Bootstrap
{
    public class Game : MonoBehaviour, IGameStateController
    {
        [SerializeField] private LoadGameData _loadGameData;
        [SerializeField] private RunTimeGameData _runTimeGameData;
        
        [Inject] private DiContainer _container;
        [Inject] private SaveSystem _saveSystem;
        [Inject] private ScoreView _scoreView;
        [Inject] private MusicSwitcher _musicSwitcher;
        
        private const float DelayEndGame = 1.5f;
        
        private GameStateMachine _gameStateMachine;
        private ScoreHandler _scoreHandler;
        
        public event Action<bool> OnPauseGame;
        public event Action OnEndGame;

        private void Awake()
        {
            _scoreHandler = new ScoreHandler(_saveSystem, _scoreView, this);
            
            _gameStateMachine = new GameStateMachine(
                _container, 
                _loadGameData, 
                _runTimeGameData,
                _scoreHandler,
                _saveSystem,
                _musicSwitcher);
            _gameStateMachine.EnterIn<LoadGameState>();
        }

        private void Update()
        {
            _gameStateMachine.UpdateSystem();
        }

        private void FixedUpdate()
        {
            _gameStateMachine.FixedUpdateSystem();
        }

        private void OnDestroy()
        {
            Physics.simulationMode = SimulationMode.Update;
            _scoreHandler.Dispose();
        }

        public void PauseGame()
        {
            OnPauseGame?.Invoke(true);
            _gameStateMachine.EnterIn<PauseGameState>();
        }

        public void ResumeGame()
        {
            OnPauseGame?.Invoke(false);
            _gameStateMachine.EnterIn<LoopGameState>();
        }

        public void EndGame()
        {
            OnEndGame?.Invoke();
            
            DOTween.Sequence()
                .AppendInterval(DelayEndGame)
                .AppendCallback(() =>
                {
                    _gameStateMachine.EnterIn<PauseGameState>();
                });
        }
    }
}
