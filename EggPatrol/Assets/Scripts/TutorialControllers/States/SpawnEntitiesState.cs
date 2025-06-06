using DG.Tweening;
using GameControllers.Bootstrap;
using GameControllers.Controllers.Properties;
using GameControllers.Factories.PoolFactoriesTypes;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Properties;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;
using GameControllers.StateMachineBasic;
using UnityEngine;
using Zenject;

namespace TutorialControllers.States
{
    public class SpawnEntitiesState : IState, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly TutorialStateMachine _tutorialStateMachine;
        private readonly UITutorialController _uiTutorialController;
        private readonly Tutorial _tutorial;
        private readonly GameSystemsHandler _gameSystemsHandler;
        private readonly ICanGetPoolEntity<GroundHaystack> _groundHaystackPool;
        private readonly ICanGetPoolEntity<FlyingHaystack> _flyingHaystackPool;
        private readonly ICanGetPoolEntity<FireGroundHaystack> _fireHaystackPoolFactory;
        private readonly ICanGetPoolEntity<Knife> _knifePoolFactory;
        private readonly ICanGetPoolEntity<ButcherKnife> _butcherKnifePoolFactory;
        private readonly ICanGetPoolEntity<Warning> _warningPool;
        private readonly Transform _groundHaystackSpawnPoint;
        private readonly Transform _flyingHaystackSpawnPoint;
        private readonly Transform _knifeSpawnPoint;
        private readonly Transform _butcherKnifeSpawnPoint;
        
        private const string WarningMessage = "Be careful when you see a warning on the edge of the screen.";
        private const string GroundHaystackMessage = "When hit, the haystack stuns and deals damage.";
        private const string FlyingHaystackMessage = "Be careful! The haystack could fall right on your head.";
        private const string FireGroundHaystackMessage = "Oh no, someone set the haystack on fire. It can still stun you on impact, but it can also set you on fire if you get too close.";
        private const string EdgedWeaponMessage = "If the knives are stuck in the ground, you can remove them by clicking on them. If the knife is stuck in you, you should remove it as soon as possible because you will take damage over time.";
        
        private bool _isGroundHaystackMessage;
        private bool _isFlyingHaystackMessage;
        private bool _isFireGroundHaystackMessage;
        private bool _isEdgedWeaponMessage;
        private bool _isStopUpdate;
        
        public SpawnEntitiesState(
            TutorialStateMachine tutorialStateMachine,
            UITutorialController uiTutorialController,
            Tutorial tutorial,
            GameSystemsHandler gameSystemsHandler,
            LoadGameData loadGameData,
            TutorialData tutorialData)
        {
            _tutorialStateMachine = tutorialStateMachine;
            _uiTutorialController = uiTutorialController;
            _tutorial = tutorial;
            _gameSystemsHandler = gameSystemsHandler;
            _groundHaystackSpawnPoint = tutorialData.GroundHaystackSpawnPoint;
            _flyingHaystackSpawnPoint = tutorialData.FlyingHaystackSpawnPoint;
            _knifeSpawnPoint = tutorialData.KnifeSpawnPoint;
            _butcherKnifeSpawnPoint = tutorialData.ButcherKnifeSpawnPoint;
            
            _groundHaystackPool = 
                new GroundHaystackPoolFactory(loadGameData.GroundHaystackPrefab, _gameSystemsHandler);
            _groundHaystackPool.CreatePoolFactory();
            
            _flyingHaystackPool = 
                new FlyingHaystackPoolFactory(loadGameData.FlyingHaystackPrefab, _gameSystemsHandler);
            _flyingHaystackPool.CreatePoolFactory();
            
            _fireHaystackPoolFactory = 
                new FireGroundHaystackPoolFactory(loadGameData.FireGroundHaystackPrefab, _gameSystemsHandler);
            _fireHaystackPoolFactory.CreatePoolFactory();

            _knifePoolFactory = 
                new KnifePoolFactory(loadGameData.KnifePrefab, _gameSystemsHandler);
            _knifePoolFactory.CreatePoolFactory();

            _butcherKnifePoolFactory = 
                new ButcherKnifePoolFactory(loadGameData.ButcherKnifePrefab, _gameSystemsHandler);
            _butcherKnifePoolFactory.CreatePoolFactory();
            
            _warningPool = 
                new WarningPoolFactory(loadGameData.WarningPrefab, _gameSystemsHandler);
            _warningPool.CreatePoolFactory();
        }

        public void Enter()
        {
            ShowWarningMessage();
        }

        public void Exit()
        {
            
        }

        public void UpdateSystem()
        {
            if (_isStopUpdate) return;
            
            _gameSystemsHandler.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            if (_isStopUpdate) return;
            
            _gameSystemsHandler.FixedUpdateSystem();
        }
        
        private void ClickEvent()
        {
            _isStopUpdate = false;
            _tutorial.OnClickToContinue -= ClickEvent;
            _uiTutorialController.HideText();
            
            if (!_isGroundHaystackMessage)
                ShowGroundHaystack();
            else if (!_isFlyingHaystackMessage)
                ShowFlyingHaystack();
            else if (!_isFireGroundHaystackMessage)
                ShowFireGroundHaystack();
            else if (!_isEdgedWeaponMessage)
                ShowEdgedWeapons();
            else
                _tutorialStateMachine.EnterIn<EndTutorialState>();
        }

        private void SpawnEntity<T>(ICanGetPoolEntity<T> poolEntity, Transform spawnPoint)
            where T : IEntity
        {
            _warningPool.GetPoolEntity(spawnPoint);
            
            DOTween.Sequence()
                .AppendInterval(Warning.WarningDelay)
                .AppendCallback(() => poolEntity.GetPoolEntity(spawnPoint));
        }

        private void ShowWarningMessage()
        {
            SpawnEntity(_groundHaystackPool, _groundHaystackSpawnPoint);
            StartMessageSequence(Warning.WarningDelay / 2f, WarningMessage);
        }
        
        private void ShowGroundHaystack()
        {
            _isGroundHaystackMessage = true;
            StartMessageSequence(1f, GroundHaystackMessage);
        }
        
        private void ShowFlyingHaystack()
        {
            SpawnEntity(_flyingHaystackPool, _flyingHaystackSpawnPoint);
            _isFlyingHaystackMessage = true;
            StartMessageSequence(3f, FlyingHaystackMessage);
        }

        private void ShowFireGroundHaystack()
        {
            SpawnEntity(_fireHaystackPoolFactory, _groundHaystackSpawnPoint);
            _isFireGroundHaystackMessage = true;
            StartMessageSequence(3f, FireGroundHaystackMessage);
        }

        private void ShowEdgedWeapons()
        {
            SpawnEntity(_knifePoolFactory, _knifeSpawnPoint);
            SpawnEntity(_butcherKnifePoolFactory, _butcherKnifeSpawnPoint);
            _isEdgedWeaponMessage = true;
            StartMessageSequence(3.5f, EdgedWeaponMessage);
        }

        private void StartMessageSequence(float delay, string message)
        {
            DOTween.Sequence()
                .AppendInterval(delay)
                .AppendCallback(() =>
                {
                    _isStopUpdate = true;
                    _uiTutorialController.ShowText(message);
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    _tutorial.OnClickToContinue += ClickEvent;
                });
        }
    }
}
