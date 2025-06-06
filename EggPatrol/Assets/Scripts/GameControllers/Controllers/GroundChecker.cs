using System;
using UnityEngine;

namespace GameControllers.Controllers
{
    [RequireComponent(typeof(Collider))]
    public class GroundChecker : MonoBehaviour
    {
        public event Action<bool> StateBeingGround;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                StateBeingGround?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                StateBeingGround?.Invoke(false);
            }
        }
    }
}
