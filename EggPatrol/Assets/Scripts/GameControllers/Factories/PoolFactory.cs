using GameControllers.Bootstrap.Properties;
using GameControllers.Controllers.Properties;
using GameControllers.Factories.Properties;
using GameControllers.Models.GameEntities.Properties;
using GameControllers.PoolObjects;
using UnityEngine;
using Zenject;

namespace GameControllers.Factories
{
    public abstract class PoolFactory<T> : ICanGetPoolEntity<T>
        where T : MonoBehaviour, IEntity
    {
        private PoolBase<T> _entitiesPool;
        
        private readonly T _entity;
        private readonly ISystemsHandler _systemsHandler;
        
        private const int EntityPreloadCount = 5;
        
        protected PoolFactory(T entity, ISystemsHandler systemsHandler)
        {
            _entity = entity;
            _systemsHandler = systemsHandler;
        }

        public void CreatePoolFactory()
        {
            _entitiesPool = new PoolBase<T>(Preload, GetEntityAction, ReturnEntityAction, EntityPreloadCount);
        }
    
        public virtual T GetPoolEntity(Transform positionAppearance)
        {
            var newEntity = _entitiesPool.Get();
            newEntity.ActiveInit(positionAppearance.position, positionAppearance.rotation);
    
            return newEntity;
        }
        
        protected virtual T Preload()
        {
            var newEntity = Object.Instantiate(_entity, new Vector3(0, 20, 0), Quaternion.identity);
            
            newEntity.SpawnInit(ReturnEntity);

            if (newEntity is IHaveUpdate entityUpdates)
                _systemsHandler.RegisterUpdateSystem(entityUpdates);
            
            return newEntity;
        }
        
        private void ReturnEntity(IEntity entity) => _entitiesPool.Return((T)entity);
    
        private void GetEntityAction(T entity) => entity.ChangeStateEntity(true);
        
        private void ReturnEntityAction(T entity) => entity.ChangeStateEntity(false);
    }
}
