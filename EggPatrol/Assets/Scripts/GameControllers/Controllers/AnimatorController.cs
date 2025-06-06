using UnityEngine;

namespace GameControllers.Controllers
{
    public abstract class AnimatorController
    {
        private float _currentAnimatorSpeed;
        
        protected readonly Animator Animator;
        
        protected AnimatorController(Animator animator)
        {
            Animator = animator;
        }
        
        public void PauseAnimator(bool isPause)
        {
            if (isPause)
            {
                _currentAnimatorSpeed = Animator.speed;
                Animator.speed = 0;
            }
            else
            {
                Animator.speed = _currentAnimatorSpeed;
            }
        }
    }
}
