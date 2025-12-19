using DG.Tweening;
using GameControllers.Bootstrap.Properties;
using GlobalSystems;
using MusicSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameControllers.Views
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image _musicIconPauseScreen;
        [SerializeField] private Image _soundIconPauseScreen;
        [SerializeField] private Image _musicIconEndGameScreen;
        [SerializeField] private Image _soundIconEndGameScreen;
        [SerializeField] private GameObject _endGameScreen;
        [SerializeField] private GameObject _endGameFrame;
        
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private MusicController _musicController;
        [Inject] private IGameStateController _gameStateController;
        [Inject] private MusicSwitcher _musicSwitcher;

        private void Start()
        {
            _gameStateController.OnEndGame += EndGame;
        }

        private void OnDestroy()
        {
            _gameStateController.OnEndGame -= EndGame;
        }

        public void ClickPause()
        {
            UpdateIcons(_musicIconPauseScreen, _soundIconPauseScreen);
            _gameStateController.PauseGame();
        }

        public void ClickResume()
        {
            _gameStateController.ResumeGame();
        }

        public void ClickRestart()
        {
            _sceneDataLoader.ChangeScene("Game");
        }

        public void ClickBackToMenu()
        {
            _sceneDataLoader.ChangeScene("MainMenu");
        }
        
        public void ClickChangeMusicState(Image icon)
        {
            _musicController.ChangeMusicState(icon);
        }

        public void ClickChangeSoundState(Image icon)
        {
            _musicController.ChangeSoundEffectsState(icon);
        }

        public void ClickButton()
        {
            _musicSwitcher.PlayClickSound();
        }

        private void UpdateIcons(Image musicIcon, Image soundIcon)
        {
            _musicController.CheckMusicState(musicIcon);
            _musicController.CheckSoundEffectsState(soundIcon);
        }

        private void EndGame()
        {
            _musicSwitcher.PlayLoseSound();
            UpdateIcons(_musicIconEndGameScreen, _soundIconEndGameScreen);

            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    _endGameScreen.SetActive(true);
                    _endGameFrame.transform.DOScale(Vector3.one, 0.5f);
                });
        }
    }
}
