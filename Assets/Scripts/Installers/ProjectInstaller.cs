using Config.Interfaces;
using Config.Services;
using UnityEngine;

namespace Installers
{
    using Zenject;

    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private bool DebugMode;

        public override void InstallBindings()
        {
            Container.Bind<IConfigReader>().To<JsonConfigReader>().AsSingle();
            Container.BindInstance(DebugMode).WhenInjectedInto<ConfigurationLoader>();
            Container.Bind<ConfigurationLoader>().AsSingle().NonLazy();
        }
    }
}