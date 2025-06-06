using GameControllers.Bootstrap.Properties;
using GameControllers.Models.GameEntities.Types.Haystacks;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class GroundHaystackPoolFactory : PoolFactory<GroundHaystack>
    {
        public GroundHaystackPoolFactory(GroundHaystack entity, ISystemsHandler systemsHandler) : base(entity, systemsHandler)
        {
        }
    }
}
