using UnityEngine;

namespace GameControllers.Models.GameEntities.Types.Haystacks
{
    public class FlyingHaystack : Haystack
    {
        protected override void ChooseDirection(Vector3 startPosition)
        {
            if (startPosition.x > 0)
            {
                DirectionMove = new Vector3(-0.5f, -0.5f, 0f);
                StartRotate(360f);
            }
            else
            {
                DirectionMove = new Vector3(0.5f, -0.5f, 0f);
                StartRotate(-360f);
            }
        }
    }
}
