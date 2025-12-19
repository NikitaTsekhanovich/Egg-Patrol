using GameControllers.Views;
using Zenject;

namespace GameControllers.Installers
{
    public class HealthViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<HealthView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
