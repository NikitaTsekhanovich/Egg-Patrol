using System;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Enums;
using GameControllers.Models.GameEntities.Properties;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameControllers.Models.GameEntities
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        private Action<IEntity> _returnAction;
        
        public bool IsActive => gameObject.activeSelf;
        
        public virtual void SpawnInit(Action<IEntity> returnAction)
        {
            _returnAction = returnAction;
        }

        public virtual void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
        
        protected virtual void ReturnToPool()
        {
            _returnAction?.Invoke(this);
        }

        public void ChangeStateEntity(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
