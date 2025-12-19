using GameControllers.StateMachineBasic;

namespace TutorialControllers.States
{
    public class MainRulesState : IState
    {
        private readonly TutorialStateMachine _tutorialStateMachine;
        private readonly UITutorialController _uiTutorialController;
        private readonly Tutorial _tutorial;
        
        private const string Message = "Your main goal is to survive as long as possible.\nIn training you will not receive damage";
        
        public MainRulesState(
            TutorialStateMachine tutorialStateMachine,
            Tutorial tutorial,
            UITutorialController uiTutorialController)
        {
            _tutorialStateMachine = tutorialStateMachine;
            _tutorial = tutorial;
            _uiTutorialController = uiTutorialController;
        }
        
        public void Enter()
        {
            _tutorial.OnClickToContinue += EndState;
            _uiTutorialController.ShowText(Message);
        }

        public void Exit()
        {
            _tutorial.OnClickToContinue -= EndState;
        }

        private void EndState()
        {
            _uiTutorialController.HideText();
            _tutorialStateMachine.EnterIn<PlayerControlState>();
        }
    }
}
