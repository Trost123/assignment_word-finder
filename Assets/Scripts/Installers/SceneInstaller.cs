using Audio;
using Controllers;
using Handlers;
using Interfaces.Audio;
using Interfaces.Input;
using Managers;
using Signals;
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

            // Signals
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<IncorrectWordSignal>();

            // Signal handlers
            Container.BindInterfacesAndSelfTo<WrongWordHandler>().AsSingle();

            // Managers
            Container.Bind<IAudioManager>().To<AudioManager>().AsSingle();
            Container.Bind<GameManager>().AsSingle().NonLazy();
        }
    }
}