using GameControllers.Bootstrap;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Factories.FactoriesTypes;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.StateMachineBasic;
using GameControllers.Views;
using MusicSystem;
using Zenject;

namespace TutorialControllers.States
{
    public class LoadTutorialState : IState
    {
        private readonly TutorialStateMachine _tutorialStateMachine;
        private readonly DiContainer _container;
        private readonly LoadGameData _loadGameData;
        private readonly GameSystemsHandler _gameSystemsHandler;
        private readonly MusicSwitcher _musicSwitcher;
        private readonly TutorialData _tutorialData;
        
        public LoadTutorialState(
            TutorialStateMachine tutorialStateMachine,
            DiContainer container,
            LoadGameData loadGameData,
            GameSystemsHandler gameSystemsHandler,
            MusicSwitcher musicSwitcher,
            TutorialData tutorialData)
        {
            _tutorialStateMachine = tutorialStateMachine;
            _container = container;
            _loadGameData = loadGameData;
            _gameSystemsHandler = gameSystemsHandler;
            _musicSwitcher = musicSwitcher;
            _tutorialData = tutorialData;
        }
        
        public void Enter()
        {
            SpawnPlayer();
            CreateInteractHandlers();
            _tutorialStateMachine.EnterIn<GreetingState>();
        }

        public void Exit()
        {
            
        }
        
        private void SpawnPlayer()
        {
            var inputController = _container.Resolve<IInputController>();
            
            ICanGetEntity<Player> playerFactory = 
                new PlayerFactory(_loadGameData.PlayerPrefab);
            
            var eggsScoreView = _container.Resolve<EggsScoreView>();
            var healthView = _container.Resolve<HealthView>();
            
            var player = playerFactory.GetEntity(_loadGameData.PlayerSpawnTransform);
            _container.Inject(player);
            player.Init(inputController, eggsScoreView, healthView, true);
            
            _gameSystemsHandler.RegisterUpdateSystem(player);
            _gameSystemsHandler.RegisterFixedUpdateSystem(player);
        }
        
        private void CreateInteractHandlers()
        {
            var clickDetector = new ClickDetector(_tutorialData.ClickParticle, null, _musicSwitcher);
            
            _gameSystemsHandler.RegisterUpdateSystem(clickDetector);
        }
    }
}
