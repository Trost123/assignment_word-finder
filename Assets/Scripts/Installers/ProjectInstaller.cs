using Config.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ConfigurationLoader>().AsSingle().NonLazy();
        }
    }
}