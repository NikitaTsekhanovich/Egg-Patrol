using UnityEngine;

namespace TutorialControllers
{
    public class HandleAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _controlPlayerPointHandle;
        [SerializeField] private Animator _animator;
        
        private static readonly int IsControlPanel = Animator.StringToHash("IsControlPanel");
        
        public void ShowControlPlayer()
        {
            gameObject.SetActive(true);
            transform.position = _controlPlayerPointHandle.position;
            _animator.SetBool(IsControlPanel, true);
        }

        public void HideControlPlayer()
        {
            _animator.SetBool(IsControlPanel, false);
            gameObject.SetActive(false);
        }
    }
}
