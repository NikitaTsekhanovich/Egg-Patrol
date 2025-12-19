using GlobalSystems;
using MusicSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenuControllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Image _musicIcon;
        [SerializeField] private Image _soundIcon;
        
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private MusicController _musicController;
        [Inject] private MusicSwitcher _musicSwitcher;

        private void Start()
        {
            CheckStartMusicIcons();
        }

        private void CheckStartMusicIcons()
        {
            _musicController.CheckMusicState(_musicIcon);
            _musicController.CheckSoundEffectsState(_soundIcon);
        }

        public void ClickPlay()
        {
            _sceneDataLoader.ChangeScene("Game");
        }

        public void ClickStartTutorial()
        {
            _sceneDataLoader.ChangeScene("Tutorial");
        }

        public void ClickChangeMusicState()
        {
            _musicController.ChangeMusicState(_musicIcon);
        }

        public void ClickChangeSoundState()
        {
            _musicController.ChangeSoundEffectsState(_soundIcon);
        }

        public void ClickButton()
        {
            _musicSwitcher.PlayClickSound();
        }
    }
}
