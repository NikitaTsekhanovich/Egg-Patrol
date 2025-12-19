using GameControllers.Views;
using Zenject;

namespace GameControllers.Installers
{
    public class ScoreViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ScoreView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
