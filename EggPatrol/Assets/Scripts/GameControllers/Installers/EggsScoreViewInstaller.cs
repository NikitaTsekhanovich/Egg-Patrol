using GameControllers.Views;
using Zenject;

namespace GameControllers.Installers
{
    public class EggsScoreViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<EggsScoreView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
