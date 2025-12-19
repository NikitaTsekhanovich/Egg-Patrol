using GameControllers.Bootstrap.Properties;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class ButcherKnifePoolFactory : PoolFactory<ButcherKnife>
    {
        public ButcherKnifePoolFactory(ButcherKnife entity, ISystemsHandler systemsHandler) : base(entity, systemsHandler)
        {
        }
    }
}
