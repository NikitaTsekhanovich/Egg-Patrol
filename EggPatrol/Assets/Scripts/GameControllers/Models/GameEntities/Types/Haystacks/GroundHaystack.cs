using DG.Tweening;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Models.GameEntities.Types.Haystacks
{
    public class GroundHaystack : Haystack
    {
        protected override void ChooseDirection(Vector3 startPosition)
        {
            if (startPosition.x > 0)
            {
                DirectionMove = Vector3.left;
                StartRotate(360f);
            }
            else
            {
                DirectionMove = Vector3.right;
                StartRotate(-360f);
            }
        }

        protected override void CollideDamageTaker(ICanTakeDamage damageTaker)
        {
            base.CollideDamageTaker(damageTaker);
            
            transform.DOKill();
            var direction = damageTaker.TargetTransform.position - transform.position;
            ChooseDirection(direction);
        }
    }
}
