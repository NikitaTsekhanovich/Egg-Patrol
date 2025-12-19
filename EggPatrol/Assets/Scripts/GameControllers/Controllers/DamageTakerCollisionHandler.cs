using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class DamageTakerCollisionHandler : MonoBehaviour
    {
        public event Action<ICanTakeDamage> CollisionDamageTaker;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICanTakeDamage>(out var damageTaker))
                CollisionDamageTaker?.Invoke(damageTaker);
        }
    }
}
