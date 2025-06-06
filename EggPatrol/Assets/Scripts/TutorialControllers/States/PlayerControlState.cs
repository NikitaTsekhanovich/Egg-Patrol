using DG.Tweening;
using GameControllers.Bootstrap;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;

namespace TutorialControllers.States
{
    public class PlayerControlState : IState, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly TutorialStateMachine _tutorialStateMachine;
        private readonly UITutorialController _uiTutorialController;
        private readonly Tutorial _tutorial;
        private readonly HandleAnimator _handleAnimator;
        private readonly GameSystemsHandler _gameSystemsHandler;
        
        private const string Message = "You can control your character using the buttons located below.";

        private bool _canUpdate;
        
        public PlayerControlState(
            TutorialStateMachine tutorialStateMachine,
            Tutorial tutorial,
            UITutorialController uiTutorialController,
            TutorialData tutorialData,
            GameSystemsHandler gameSystemsHandler)
        {
            _tutorialStateMachine = tutorialStateMachine;
            _tutorial = tutorial;
            _uiTutorialController = uiTutorialController;
            _handleAnimator = tutorialData.HandleAnimator;
            _gameSystemsHandler = gameSystemsHandler;
        }
        
        public void Enter()
        {
            _tutorial.OnClickToContinue += ClickEvent;
            _uiTutorialController.ShowText(Message);
        }
        
        public void UpdateSystem()
        {
            if (!_canUpdate) return;
            
            _gameSystemsHandler.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            if (!_canUpdate) return;
            
            _gameSystemsHandler.FixedUpdateSystem();
        }

        public void Exit()
        {
            
        }

        private void ClickEvent()
        {
            _tutorial.OnClickToContinue -= ClickEvent;
            _canUpdate = true;
            _handleAnimator.ShowControlPlayer();
            _uiTutorialController.HideText();
            StartEndTimer();
        }

        private void StartEndTimer()
        {
            DOTween.Sequence()
                .AppendInterval(5f)
                .AppendCallback(() =>
                {
                    _handleAnimator.HideControlPlayer();
                    _tutorialStateMachine.EnterIn<EggSpawnState>();
                });
        }
    }
}
