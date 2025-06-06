using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class ScoreIncreaserCollisionHandler : MonoBehaviour
    {
        public event Action<ICanIncreaseScore> CollisionScoreIncreaser;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICanIncreaseScore>(out var scoreIncreaser))
                CollisionScoreIncreaser?.Invoke(scoreIncreaser);
        }
    }
}
