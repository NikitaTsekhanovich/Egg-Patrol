using GameControllers.Bootstrap.Properties;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class KnifePoolFactory : PoolFactory<Knife>
    {
        public KnifePoolFactory(Knife entity, ISystemsHandler systemsHandler) : base(entity, systemsHandler)
        {
        }
    }
}
