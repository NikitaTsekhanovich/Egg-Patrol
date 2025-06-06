using GameControllers.StateMachineBasic;

namespace TutorialControllers.States
{
    public class GreetingState : IState
    {
        private readonly TutorialStateMachine _tutorialStateMachine;
        private readonly UITutorialController _uiTutorialController;
        private readonly Tutorial _tutorial;
        
        private const string Message = "Welcome to Egg Patrol!\nLet's go through a short tutorial.";
        
        public GreetingState(
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
            _tutorialStateMachine.EnterIn<MainRulesState>();
        }
    }
}
