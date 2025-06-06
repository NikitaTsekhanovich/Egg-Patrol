using GameControllers.Controllers.Properties;
using GameControllers.Views;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class EggsScoreHandler : ICanRequestSave
    {
        private readonly EggsScoreView _eggsScoreView;
        private readonly SaveSystem _saveSystem;
        private readonly AudioSource _increaseScoreSound;
        
        private int _currentScore;
        
        public EggsScoreHandler(
            EggsScoreView eggsScoreView, 
            SaveSystem saveSystem,
            AudioSource increaseScoreSound)
        {
            _eggsScoreView = eggsScoreView;
            _saveSystem = saveSystem;
            _increaseScoreSound = increaseScoreSound;
        }

        public void IncreaseEggs(int eggs)
        {
            _increaseScoreSound.Play();
            _currentScore += eggs;
            _eggsScoreView.UpdateEggs(_currentScore);
        }

        public void RequestSave()
        {
            var savedValue = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDEggsCount);
            _saveSystem.SaveData<PlayerSaveData, int>(savedValue + _currentScore, PlayerSaveData.GUIDEggsCount);
        }
    }
}
