using Controllers;
using Managers;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().AsSingle().NonLazy();
        }
    }
}