using UnityEngine;

namespace GameControllers.Factories.Properties
{
    public interface ICanGetPoolEntity<T>
    {
        public void CreatePoolFactory();
        public T GetPoolEntity(Transform positionAppearance);
    }
}
