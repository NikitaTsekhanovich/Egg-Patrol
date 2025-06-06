using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Factories.FactoriesTypes;
using GameControllers.Factories.PoolFactoriesTypes;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;
using GameControllers.SpawnerEntitiesStateMachine;
using GameControllers.StateMachineBasic;
using GameControllers.Views;
using MusicSystem;
using SaveSystems;
using Zenject;

namespace GameControllers.Bootstrap.StateMachine.States
{
    public class LoadGameState : IState 
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _container;
        private readonly LoadGameData _loadGameData;
        private readonly RunTimeGameData _runTimeGameData;
        private readonly GameSystemsHandler _gameSystemsHandler;
        private readonly ScoreHandler _scoreHandler;
        private readonly SaveSystem _saveSystem;
        private readonly MusicSwitcher _musicSwitcher;
        
        public LoadGameState(
            GameStateMachine gameStateMachine,
            DiContainer container, 
            LoadGameData loadGameData,
            RunTimeGameData runTimeGameData,
            GameSystemsHandler gameSystemsHandler,
            ScoreHandler scoreHandler,
            SaveSystem saveSystem,
            MusicSwitcher musicSwitcher)
        {
            _gameStateMachine = gameStateMachine;
            _container = container;
            _loadGameData = loadGameData;
            _runTimeGameData = runTimeGameData;
            _gameSystemsHandler = gameSystemsHandler;
            _scoreHandler = scoreHandler;
            _saveSystem = saveSystem;
            _musicSwitcher = musicSwitcher;
        }
        
        public void Enter()
        {
            CreateSpawnerStateMachine();
            CreateInteractHandlers();
            SpawnPlayer();
            _gameStateMachine.EnterIn<LoopGameState>();
        }

        public void Exit()
        {
            
        }

        private void CreateSpawnerStateMachine()
        {
            ICanGetPoolEntity<Egg> eggPool = 
                new EggPoolFactory(_loadGameData.EggPrefab, _gameSystemsHandler, _container);
            eggPool.CreatePoolFactory();
            
            ICanGetPoolEntity<GroundHaystack> groundHaystackPool =
                new GroundHaystackPoolFactory(_loadGameData.GroundHaystackPrefab, _gameSystemsHandler);
            groundHaystackPool.CreatePoolFactory();
            
            ICanGetPoolEntity<FireGroundHaystack> fireGroundHaystackPool = 
                new FireGroundHaystackPoolFactory(_loadGameData.FireGroundHaystackPrefab, _gameSystemsHandler);
            fireGroundHaystackPool.CreatePoolFactory();
            
            ICanGetPoolEntity<FlyingHaystack> flyingHaystackPool = 
                new FlyingHaystackPoolFactory(_loadGameData.FlyingHaystackPrefab, _gameSystemsHandler);
            flyingHaystackPool.CreatePoolFactory();
            
            ICanGetPoolEntity<ButcherKnife> butcherKnifePool = 
                new ButcherKnifePoolFactory(_loadGameData.ButcherKnifePrefab, _gameSystemsHandler);
            butcherKnifePool.CreatePoolFactory();
            
            ICanGetPoolEntity<Knife> knifePool =
                new KnifePoolFactory(_loadGameData.KnifePrefab, _gameSystemsHandler);
            knifePool.CreatePoolFactory();
            
            ICanGetPoolEntity<Warning> warningPool =
                new WarningPoolFactory(_loadGameData.WarningPrefab, _gameSystemsHandler);
            warningPool.CreatePoolFactory();
            
            var spawnerStateMachine = new SpawnerStateMachine(
                eggPool, 
                groundHaystackPool, 
                flyingHaystackPool,
                fireGroundHaystackPool,
                butcherKnifePool,
                knifePool,
                warningPool,
                _runTimeGameData,
                _scoreHandler);
            
            _gameSystemsHandler.RegisterUpdateSystem(spawnerStateMachine);
        }

        private void CreateInteractHandlers()
        {
            var destroyObjectAbilityView = _container.Resolve<DestroyAbilityView>();
            var destroyObjectAbility = new DestroyAbility(_saveSystem, destroyObjectAbilityView);
            
            var clickDetector = new ClickDetector(_runTimeGameData.ClickParticle, destroyObjectAbility, _musicSwitcher);
            
            _gameSystemsHandler.RegisterUpdateSystem(clickDetector);
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
            player.Init(inputController, eggsScoreView, healthView);
            
            _gameSystemsHandler.RegisterUpdateSystem(player);
            _gameSystemsHandler.RegisterFixedUpdateSystem(player);
        }
    }
}
