using GameControllers.Controllers.Properties;

namespace GameControllers.Bootstrap.Properties
{
    public interface ISystemsHandler
    {
        public void RegisterUpdateSystem(IHaveUpdate system);
        public void UnregisterUpdateSystem(IHaveUpdate system);
        public void RegisterFixedUpdateSystem(IHaveFixedUpdate system);
        public void UnregisterFixedUpdateSystem(IHaveFixedUpdate system);
    }
}
