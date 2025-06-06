namespace GameControllers.StateMachineBasic
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}
