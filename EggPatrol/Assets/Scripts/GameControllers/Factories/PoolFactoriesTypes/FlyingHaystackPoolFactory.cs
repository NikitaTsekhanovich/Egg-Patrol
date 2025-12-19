using GameControllers.Bootstrap.Properties;
using GameControllers.Models.GameEntities.Types.Haystacks;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class FlyingHaystackPoolFactory : PoolFactory<FlyingHaystack>
    {
        public FlyingHaystackPoolFactory(FlyingHaystack entity, ISystemsHandler systemsHandler) : base(entity, systemsHandler)
        {
        }
    }
}
