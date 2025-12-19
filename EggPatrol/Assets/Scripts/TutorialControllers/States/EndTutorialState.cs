using DG.Tweening;
using GameControllers.Bootstrap;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;
using GlobalSystems;

namespace TutorialControllers.States
{
    public class EndTutorialState : IState, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly UITutorialController _uiTutorialController;
        private readonly Tutorial _tutorial;
        private readonly GameSystemsHandler _gameSystemsHandler;
        private readonly SceneDataLoader _sceneDataLoader;
        
        private const string FirstMessage = "In this game, you will have several difficult stages.With each new stage, your score will increase faster.";
        private const string SecondMessage = "Survive as long as possible, improve your score, buy abilities for eggs, enjoy the game!";

        private bool _isSecondMessage;
        private bool _isStopUpdate;

        public EndTutorialState(
            UITutorialController uiTutorialController,
            Tutorial tutorial,
            GameSystemsHandler gameSystemsHandler,
            SceneDataLoader sceneDataLoader)
        {
            _uiTutorialController = uiTutorialController;
            _tutorial = tutorial;
            _gameSystemsHandler = gameSystemsHandler;
            _sceneDataLoader = sceneDataLoader;
        }
        
        public void Enter()
        {
            StartMessageSequence(5.5f, FirstMessage);
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
            
            if (!_isSecondMessage)
                ShowSecondMessage();
            else
                EndTutorial();
        }

        private void EndTutorial()
        {
            _sceneDataLoader.ChangeScene("MainMenu");
        }

        private void ShowSecondMessage()
        {
            _isSecondMessage = true;
            StartMessageSequence(0f, SecondMessage);
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
