using System;
using UnityEngine;

namespace GameControllers.Models.GameEntities.Properties
{
    public interface IEntity
    {
        public void SpawnInit(Action<IEntity> returnAction);
        public void ActiveInit(Vector3 startPosition, Quaternion startRotation);
        public void ChangeStateEntity(bool state);
    }
}
