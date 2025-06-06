using DG.Tweening;
using GameControllers.Models.GameEntities;
using UnityEngine;

namespace GameControllers.Models
{
    public class Warning : Entity
    {
        private readonly Vector3 _scale = new (0.017f, 0.017f, 0.017f);
        
        private const float OffsetX = 0.12f;
        private const float MaxPositionY = 1.282f;
        private const float BorderOffsetX = 0.19f;
        
        private Sequence _activeSequence;
        
        public const float WarningDelay = 1.5f;

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            var offsetX = OffsetX;
            var positionY = Mathf.Clamp(startPosition.y, 0f, MaxPositionY);
            
            if (Mathf.Abs(startPosition.x) < BorderOffsetX)
                offsetX *= 0;
            else if (startPosition.x > 0)
                offsetX *= -1;
            
            base.ActiveInit(
                new Vector3(
                    startPosition.x + offsetX,
                    positionY,
                    startPosition.z), 
                startRotation);
            
            StartActiveSequence();
        }

        private void StartActiveSequence()
        {
            transform.DOScale(_scale, 0.3f);
            
            _activeSequence = DOTween.Sequence()
                .Append(transform.DOShakeRotation(WarningDelay, new Vector3(0f, 0f, 30f)))
                .SetEase(Ease.Linear)
                .Append(transform.DOScale(Vector3.zero, 0.3f))
                .AppendCallback(ReturnToPool);
        }

        private void OnDestroy()
        {
            transform.DOKill();
            _activeSequence.Kill();
        }
    }
}
