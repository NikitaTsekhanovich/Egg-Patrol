using GameControllers.Bootstrap.Properties;
using GameControllers.Models.GameEntities.Types;
using Zenject;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class EggPoolFactory : PoolFactory<Egg>
    {
        private readonly DiContainer _container;
        
        public EggPoolFactory(Egg entity, ISystemsHandler systemsHandler, DiContainer container) : base(entity, systemsHandler)
        {
            _container = container;
        }

        protected override Egg Preload()
        {
            var newEgg = base.Preload();
            _container.Inject(newEgg);
            return newEgg;
        }
    }
}
