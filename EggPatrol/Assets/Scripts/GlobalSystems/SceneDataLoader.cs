using MusicSystem;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine.SceneManagement;
using Zenject;

namespace GlobalSystems
{
    public class SceneDataLoader
    {
        private readonly LoadingScreenController _loadingScreenController;
        
        private string _nameScene;
        private MusicSwitcher _musicSwitcher;
        private SaveSystem _saveSystem;
        
        public SceneDataLoader(LoadingScreenController loadingScreenController)
        {
            _loadingScreenController = loadingScreenController;
        }

        [Inject]
        private void Constructor(MusicSwitcher musicSwitcher, SaveSystem saveSystem)
        {
            _musicSwitcher = musicSwitcher;
            _saveSystem = saveSystem;

            var isFirstLaunch = _saveSystem.GetData<PlayerGlobalData, bool>(PlayerGlobalData.GUIDFirstLoad);
            if (isFirstLaunch)
            {
                _saveSystem.SaveData<PlayerGlobalData, bool>(false, PlayerGlobalData.GUIDFirstLoad);
                ChangeScene("Tutorial");
            }
            else
            {
                ChangeScene("MainMenu");
            }
        }
        
        public void ChangeScene(string nameScene)
        {
            _nameScene = nameScene;
            _loadingScreenController.StartAnimationFade(LoadScene);

            if (nameScene == "MainMenu")
                _musicSwitcher.PlayMenuBackgroundMusic();
            else if (nameScene == "Game")
                _musicSwitcher.PlayGameBackgroundMusic();
        }

        private void LoadScene()
        {
            SceneManager.LoadSceneAsync(_nameScene);
        }
    }
}
