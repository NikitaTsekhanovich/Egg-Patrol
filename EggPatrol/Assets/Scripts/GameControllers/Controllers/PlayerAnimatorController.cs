using UnityEngine;

namespace GameControllers.Controllers
{
    public class PlayerAnimatorController : AnimatorController
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsJump = Animator.StringToHash("IsJump");
        private static readonly int IsSlip = Animator.StringToHash("IsSlip");
        private static readonly int IsStun = Animator.StringToHash("IsStun");
        private static readonly int FromHit = Animator.StringToHash("IsDiedFromHit");

        private bool _isPlayingRun;
        private float _currentAnimatorSpeed;


        public PlayerAnimatorController(Animator animator) : base(animator)
        {
            
        }

        public void Run(bool isRun)
        {
            if (!_isPlayingRun || !isRun)
            {
                _isPlayingRun = isRun;
                Animator.SetBool(IsRun, isRun);
            }
        }

        public void Jump(bool isJump)
        {
            Animator.SetBool(IsJump, isJump);
        }

        public void Slip(bool isSlip)
        {
            Animator.SetBool(IsSlip, isSlip);
        }

        public void Stun(bool isStun)
        {
            Animator.SetBool(IsStun, isStun);
        }

        public void DiedFromHit()
        {
            Animator.SetBool(FromHit, true);
        }
    }
}
