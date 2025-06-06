using System;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Enums;
using GameControllers.Models.GameEntities.Properties;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameControllers.Models.GameEntities.Types.Haystacks
{
    public class FireGroundHaystack : GroundHaystack
    {
        [Header("Fire Ground Haystack Properties")]
        [SerializeField] private PeriodicDamageTakerCollisionHandler _periodicDamageTakerCollisionHandler;
        [SerializeField] private int _burnDamage;
        [SerializeField] private float _burnTime;
        [SerializeField] private float _periodicBurnDamageTime;
        [SerializeField] private DamageType _periodicDamageType;

        public override void SpawnInit(Action<IEntity> returnAction)
        {
            base.SpawnInit(returnAction);
            _periodicDamageTakerCollisionHandler.CollisionPeriodicDamageTaker += PeriodicDamageObject;
        }

        private void PeriodicDamageObject(ICanTakePeriodicDamage takePeriodicDamageTaker)
        {
            takePeriodicDamageTaker.TakePeriodicDamage(_periodicDamageType, _burnDamage, _periodicBurnDamageTime, _burnTime);
        }

        protected override void OnDestroy()
        {
            _periodicDamageTakerCollisionHandler.CollisionPeriodicDamageTaker -= PeriodicDamageObject;
            base.OnDestroy();
        }
    }
}
