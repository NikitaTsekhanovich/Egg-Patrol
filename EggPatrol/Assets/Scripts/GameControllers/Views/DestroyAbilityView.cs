using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Views
{
    public class DestroyAbilityView : MonoBehaviour
    {
        [SerializeField] private Image _firstAbilityImage;
        [SerializeField] private Image _secondAbilityImage;
        [SerializeField] private Image _thirdAbilityImage;
        
        private const float FillTime = 1f;
        
        public void SetAbilities(int countAbilities)
        {
            if (countAbilities == 3)
                _thirdAbilityImage.enabled = true;
            if (countAbilities >= 2)
                _secondAbilityImage.enabled = true;
            if (countAbilities >= 1)
                _firstAbilityImage.enabled = true;
        }
        
        public void UpdateCountAbilities(int countAbilities)
        {
            switch (countAbilities)
            {
                case 2:
                    _thirdAbilityImage.DOFillAmount(0f, FillTime);
                    break;
                case 1:
                    _secondAbilityImage.DOFillAmount(0f, FillTime);
                    break;
                case 0:
                    _firstAbilityImage.DOFillAmount(0f, FillTime);
                    break;
            }
        }
    }
}
