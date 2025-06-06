using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class PeriodicDamageTakerCollisionHandler : MonoBehaviour
    {
        public event Action<ICanTakePeriodicDamage> CollisionPeriodicDamageTaker;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICanTakePeriodicDamage>(out var periodicDamageTaker))
                CollisionPeriodicDamageTaker?.Invoke(periodicDamageTaker);
        }
    }
}
