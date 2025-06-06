using UnityEngine;

namespace GameControllers.Factories.Properties
{
    public interface ICanGetEntity<T>
    {
        public T GetEntity(Transform transformSpawn);
    }
}
