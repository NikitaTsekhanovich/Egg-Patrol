using System;
using GameControllers.Bootstrap.Properties;
using GameControllers.Views;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class ScoreHandler : IDisposable
    {
        private readonly SaveSystem _saveSystem;
        private readonly ScoreView _scoreView;
        private readonly IGameStateController _gameStateController;
        private readonly int _currentBestScore;

        private float _currentScoreIncreaser;
        private int _currentScore;
        private float _currentTime;
        
        public ScoreHandler(SaveSystem saveSystem, ScoreView scoreView, IGameStateController gameStateController)
        {
            _saveSystem = saveSystem;
            _scoreView = scoreView;
            _gameStateController = gameStateController;

            _currentBestScore = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDBestScore);
            
            _gameStateController.OnEndGame += UpdateBestScore;
        }

        public void SetScoreIncreaser(float scoreIncreaser)
        {
            _currentScoreIncreaser = scoreIncreaser;
        }

        public void UpdateScore()
        {
            _currentTime += Time.deltaTime;
            
            if (_currentTime >= _currentScoreIncreaser)
            {
                _currentTime = 0f;
                _currentScore++;
                _scoreView.UpdateScore(_currentScore);
            }
        }

        private void UpdateBestScore()
        {
            if (_currentScore > _currentBestScore)
            {
                _saveSystem.SaveData<PlayerSaveData, int>(_currentScore, PlayerSaveData.GUIDBestScore);
                _scoreView.UpdateBestScore(_currentScore);
            }
            else
            {
                _scoreView.UpdateBestScore(_currentBestScore);
            }
        }

        public void Dispose()
        {
            _gameStateController.OnEndGame -= UpdateBestScore;
        }
    }
}
