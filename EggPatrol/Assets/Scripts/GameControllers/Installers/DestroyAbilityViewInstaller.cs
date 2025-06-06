using GameControllers.Views;
using Zenject;

namespace GameControllers.Installers
{
    public class DestroyAbilityViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<DestroyAbilityView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
