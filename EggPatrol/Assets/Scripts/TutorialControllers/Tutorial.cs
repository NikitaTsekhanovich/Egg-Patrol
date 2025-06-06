using System;
using GameControllers.Bootstrap.Properties;
using GameControllers.Models.DataContainers;
using GlobalSystems;
using MusicSystem;
using UnityEngine;
using Zenject;

namespace TutorialControllers
{
    public class Tutorial : MonoBehaviour, IGameStateController
    {
        [SerializeField] private UITutorialController _uiTutorialController;
        [SerializeField] private LoadGameData _loadGameData;
        [SerializeField] private TutorialData _tutorialData;
        
        [Inject] private DiContainer _container;
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private MusicSwitcher _musicSwitcher;
        
        private TutorialStateMachine _tutorialStateMachine;
        
        public event Action<bool> OnPauseGame;
        public event Action OnEndGame;
        public event Action OnClickToContinue; 
        
        private void Awake()
        {
            _tutorialStateMachine = new TutorialStateMachine(
                this,
                _container,
                _loadGameData,
                _uiTutorialController,
                _tutorialData,
                _sceneDataLoader,
                _musicSwitcher);
        }
        
        private void Update()
        {
            _tutorialStateMachine.UpdateSystem();
            CheckClick();
        }

        private void FixedUpdate()
        {
            _tutorialStateMachine.FixedUpdateSystem();
        }

        private void OnDestroy()
        {
            Physics.simulationMode = SimulationMode.Update;
        }

        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
                OnClickToContinue?.Invoke();
        }
        
        public void PauseGame()
        {
            
        }

        public void ResumeGame()
        {
            
        }

        public void EndGame()
        {
            
        }
    }
}
