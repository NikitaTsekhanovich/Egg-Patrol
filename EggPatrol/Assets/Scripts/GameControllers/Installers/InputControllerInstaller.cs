using GameControllers.Controllers.Properties;
using Zenject;

namespace GameControllers.Installers
{
    public class InputControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInputController>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
