using Config.Interfaces;
using Config.Services;

namespace Installers
{
    using Zenject;

    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IConfigReader>().To<JsonConfigReader>().AsSingle();
        }
    }
}