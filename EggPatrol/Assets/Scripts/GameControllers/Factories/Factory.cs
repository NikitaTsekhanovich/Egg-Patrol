using GameControllers.Factories.Properties;
using UnityEngine;

namespace GameControllers.Factories
{
    public abstract class Factory<T> : ICanGetEntity<T>
        where T : MonoBehaviour
    {
        public abstract T GetEntity(Transform transformSpawn);
    }
}
