using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Models
{
    public class InsideEggCollisionHandler : MonoBehaviour, IClickableObject
    {
        public event Action RemoveInsideEgg;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICanInteractInsideEgg interactInsideEgg))
            {
                interactInsideEgg.InteractInsideEgg();
                RemoveInsideEgg?.Invoke();
            }
        }

        public bool TryReact()
        {
            RemoveInsideEgg?.Invoke();
            return true;
        }
    }
}
