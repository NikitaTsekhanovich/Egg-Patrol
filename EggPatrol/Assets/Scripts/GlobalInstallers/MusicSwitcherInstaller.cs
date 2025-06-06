using MusicSystem;
using UnityEngine;
using Zenject;

namespace GlobalInstallers
{
    public class MusicSwitcherInstaller : MonoInstaller
    {
        [SerializeField] private MusicSwitcher _musicSwitcher;
        
        public override void InstallBindings()
        {
            var musicSwitcher = Instantiate(_musicSwitcher);
            
            Container
                .Bind<MusicSwitcher>()
                .FromInstance(musicSwitcher)
                .AsSingle()
                .NonLazy();
        }
    }
}
