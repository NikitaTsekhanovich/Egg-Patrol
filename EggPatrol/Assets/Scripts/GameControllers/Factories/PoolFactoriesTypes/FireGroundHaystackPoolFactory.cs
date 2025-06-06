using GameControllers.Bootstrap.Properties;
using GameControllers.Models.GameEntities.Types.Haystacks;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class FireGroundHaystackPoolFactory : PoolFactory<FireGroundHaystack>
    {
        public FireGroundHaystackPoolFactory(FireGroundHaystack entity, ISystemsHandler systemsHandler) : base(entity, systemsHandler)
        {
        }
    }
}
