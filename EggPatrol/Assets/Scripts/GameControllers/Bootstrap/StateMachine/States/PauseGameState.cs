using DG.Tweening;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.Bootstrap.StateMachine.States
{
    public class PauseGameState : IState
    {
        public void Enter()
        {
            DOTween.PauseAll();
            Physics.simulationMode = SimulationMode.Script;
        }

        public void Exit()
        {
            DOTween.PlayAll();
            Physics.simulationMode = SimulationMode.Update;
        }
    }
}
