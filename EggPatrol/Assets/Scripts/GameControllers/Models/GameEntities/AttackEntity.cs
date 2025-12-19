using System;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Enums;
using GameControllers.Models.GameEntities.Properties;
using UnityEngine;

namespace GameControllers.Models.GameEntities
{
    public class AttackEntity : Entity
    {
        [Header("AttackEntity Properties")]
        [SerializeField] private DamageTakerCollisionHandler _damageTakerCollisionHandler;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private int _damageValue;
        
        public override void SpawnInit(Action<IEntity> returnAction)
        {
            base.SpawnInit(returnAction);
            _damageTakerCollisionHandler.CollisionDamageTaker += CollideDamageTaker;
        }
        
        protected virtual void CollideDamageTaker(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(_damageType, _damageValue);
        }

        protected virtual void OnDestroy()
        {
            _damageTakerCollisionHandler.CollisionDamageTaker -= CollideDamageTaker;
        }
    }
}
