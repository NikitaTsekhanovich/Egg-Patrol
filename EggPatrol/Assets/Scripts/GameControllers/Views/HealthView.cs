using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Views
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _firstHealthImage;
        [SerializeField] private Image _secondHealthImage;
        [SerializeField] private Image _thirdHealthImage;

        private const float FillTime = 1f;

        private void OnDestroy()
        {
            _firstHealthImage.DOKill();
            _secondHealthImage.DOKill();
            _thirdHealthImage.DOKill();
            _healthBar.DOKill();
        }

        public void SetHearts(int countHeart)
        {
            if (countHeart == 3)
                _thirdHealthImage.enabled = true;
            if (countHeart >= 2)
                _secondHealthImage.enabled = true;
            if (countHeart >= 1)
                _firstHealthImage.enabled = true;
        }

        public void UpdateHealth(float health)
        {
            if (_healthBar.IsActive())
                _healthBar.DOKill();
            
            if (health != 0)
                _healthBar.DOFillAmount(health, FillTime);
            else
                _healthBar.fillAmount = 0f;
        }

        public void UpdateHearts(int countHeart)
        {
            switch (countHeart)
            {
                case 2:
                    _thirdHealthImage.DOFillAmount(0f, FillTime);
                    break;
                case 1:
                    _secondHealthImage.DOFillAmount(0f, FillTime);
                    break;
                case 0:
                    _firstHealthImage.DOFillAmount(0f, FillTime);
                    break;
            }
        }
    }
}
