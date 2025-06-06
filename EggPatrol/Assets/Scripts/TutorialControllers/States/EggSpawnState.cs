using DG.Tweening;
using GameControllers.Bootstrap;
using GameControllers.Controllers.Properties;
using GameControllers.Factories.PoolFactoriesTypes;
using GameControllers.Factories.Properties;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.StateMachineBasic;
using UnityEngine;
using Zenject;

namespace TutorialControllers.States
{
    public class EggSpawnState : IState, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly TutorialStateMachine _tutorialStateMachine;
        private readonly UITutorialController _uiTutorialController;
        private readonly Tutorial _tutorial;
        private readonly GameSystemsHandler _gameSystemsHandler;
        private readonly ICanGetPoolEntity<Egg> _eggPool;
        private readonly Transform _eggSpawnPoint;
        
        private const string FirstMessage = "You can catch eggs and buy abilities with eggs in the shop, which is located in the menu.";
        private const string SecondMessage = "If the egg falls, it will break. You can slip on it. Also, you can remove the broken egg if you click on it.";

        private bool _isSecondMessage;
        private bool _isStopUpdate;
        
        public EggSpawnState(
            TutorialStateMachine tutorialStateMachine,
            UITutorialController uiTutorialController,
            Tutorial tutorial,
            GameSystemsHandler gameSystemsHandler,
            LoadGameData loadGameData,
            DiContainer container,
            TutorialData tutorialData)
        {
            _tutorialStateMachine = tutorialStateMachine;
            _uiTutorialController = uiTutorialController;
            _tutorial = tutorial;
            _gameSystemsHandler = gameSystemsHandler;
            _eggSpawnPoint = tutorialData.EggSpawnPoint;
            
            _eggPool = new EggPoolFactory
                (loadGameData.EggPrefab, _gameSystemsHandler, container);
            _eggPool.CreatePoolFactory();
        }
        
        public void Enter()
        {
            _eggPool.GetPoolEntity(_eggSpawnPoint);
            WaitEgg();
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

        public void Exit()
        {
            
        }

        private void WaitEgg()
        {
            DOTween.Sequence()
                .AppendInterval(3.5f)
                .AppendCallback(() =>
                {
                    _isStopUpdate = true;
                    _uiTutorialController.ShowText(FirstMessage);
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    _tutorial.OnClickToContinue += ClickEvent;
                });
        }
        
        private void ClickEvent()
        {
            _isStopUpdate = false;
            _tutorial.OnClickToContinue -= ClickEvent;
            _uiTutorialController.HideText();
            
            if (!_isSecondMessage)
                ShowBrokenEggWarning();
            else
             _tutorialStateMachine.EnterIn<SpawnEntitiesState>();
        }

        private void ShowBrokenEggWarning()
        {
            _isSecondMessage = true;
            
            DOTween.Sequence()
                .AppendInterval(4f)
                .AppendCallback(() =>
                {
                    _isStopUpdate = true;
                    _uiTutorialController.ShowText(SecondMessage);
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    _tutorial.OnClickToContinue += ClickEvent;
                });
        }
    }
}
