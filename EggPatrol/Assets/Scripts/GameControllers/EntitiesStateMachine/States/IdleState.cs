using DG.Tweening;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine.States
{
    public class IdleState : IState
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _idleRotation = new (0f, 180f, 0f);
        
        public IdleState(Transform transform, Rigidbody rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }
        
        public void Enter()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _transform.DORotate(_idleRotation, PlayerStateMachine.RotateTime);
        }

        public void Exit()
        {
            _transform.DOKill();
        }
    }
}
