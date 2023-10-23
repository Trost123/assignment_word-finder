using System.Threading.Tasks;
using Config.Services;
using UnityEngine;
using Zenject;

namespace Initialization
{
    public static class GameInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // Get a reference to the DiContainer
            DiContainer container = ProjectContext.Instance.Container;

            // Resolve the ConfigurationLoader
            ConfigurationLoader configurationLoader = container.Resolve<ConfigurationLoader>();

            // Load the configuration
            configurationLoader.LoadConfigurationAsync("Assets/Config/grid_config.json").ContinueWith(loadConfigTask =>
            {
                if (loadConfigTask.IsFaulted || loadConfigTask.IsCanceled)
                {
                    Debug.LogError(loadConfigTask.Exception);
                }
                else
                {
                    // Further initialization...
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}