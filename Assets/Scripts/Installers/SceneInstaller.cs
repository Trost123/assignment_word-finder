using Audio;
using Controllers;
using Handlers;
using Input;
using Interfaces.Audio;
using Interfaces.Input;
using Interfaces.UI;
using Managers;
using Signals;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Find components in scene
            Container.Bind<GridController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUserInputHandler>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioSource>().FromComponentInHierarchy().AsSingle();

            // Managers
            Container.Bind<IAudioManager>().To<AudioManager>().AsSingle();
            Container.Bind<GameManager>().AsSingle().NonLazy();

            // Signals
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<IncorrectWordSignal>();
            
            // Signal handlers
            Container.BindInterfacesAndSelfTo<WrongWordHandler>().AsSingle();
        }
    }
}