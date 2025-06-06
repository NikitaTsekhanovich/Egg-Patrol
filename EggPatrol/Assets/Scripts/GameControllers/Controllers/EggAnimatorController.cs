using UnityEngine;

namespace GameControllers.Controllers
{
    public class EggAnimatorController : AnimatorController
    {
        private static readonly int IsBreak = Animator.StringToHash("IsBreak");

        public EggAnimatorController(Animator animator) : base(animator)
        {
        }

        public void Break(bool isBreak)
        {
            Animator.SetBool(IsBreak, isBreak);
        }
    }
}
