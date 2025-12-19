using GameControllers.Models.Enums;
using UnityEngine;

namespace GameControllers.Controllers.Properties
{
    public interface ICanTakeDamage
    {
        public Transform TargetTransform { get; }
        public void TakeDamage(DamageType damageType, int damage);
    }
}
