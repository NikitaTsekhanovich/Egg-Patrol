using GameControllers.Bootstrap.Properties;
using Zenject;

namespace GameControllers.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IGameStateController>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
