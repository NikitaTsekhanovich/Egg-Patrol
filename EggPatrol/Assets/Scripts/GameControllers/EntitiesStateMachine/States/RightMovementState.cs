using DG.Tweening;
using GameControllers.Controllers;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine.States
{
    public class RightMovementState : PhysicalMovementState
    {
        private readonly Vector3 _rotateDirection = new (0f, 90f, 0f);
        
        public RightMovementState(Rigidbody rigidbody, float speed, float maxVelocity, PlayerAnimatorController playerAnimatorController, Transform transform, GroundChecker groundChecker, AudioSource runSound) : base(rigidbody, speed, maxVelocity, playerAnimatorController, transform, groundChecker, runSound)
        {
        }

        protected override void SetDirection()
        {
            Direction = Vector3.right;
            Transform.DORotate(_rotateDirection, PlayerStateMachine.RotateTime);
        }
    }
}
