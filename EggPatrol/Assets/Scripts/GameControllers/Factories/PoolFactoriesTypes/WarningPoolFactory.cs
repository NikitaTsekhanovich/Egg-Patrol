using GameControllers.Bootstrap.Properties;
using GameControllers.Models;

namespace GameControllers.Factories.PoolFactoriesTypes
{
    public class WarningPoolFactory : PoolFactory<Warning>
    {
        public WarningPoolFactory(Warning entity, ISystemsHandler systemsHandler) : base(entity, systemsHandler)
        {
        }
    }
}
