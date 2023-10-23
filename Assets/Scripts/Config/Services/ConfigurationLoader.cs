using System.Threading.Tasks;
using Config.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Config.Services
{
    [UsedImplicitly]
    public class ConfigurationLoader
    {
        private readonly IConfigReader _configReader;
        private readonly bool _debugMode;

        public ConfigurationLoader(IConfigReader configReader, bool debugMode)
        {
            _configReader = configReader;
            _debugMode = debugMode;
        }

        public Task LoadConfigurationAsync(string path)
        {
            var tcs = new TaskCompletionSource<bool>();

            _configReader.LoadConfig(path).ContinueWith(loadConfigTask =>
            {
                if (loadConfigTask.IsFaulted || loadConfigTask.IsCanceled)
                {
                    if (loadConfigTask.Exception != null) tcs.SetException(loadConfigTask.Exception);
                }
                else
                {
                    var gridConfig = loadConfigTask.Result;
                    if (_debugMode)
                    {
                        Debug.Log(JsonUtility.ToJson(gridConfig, true));
                    }

                    // Further processing of gridConfig if necessary...

                    tcs.SetResult(true);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return tcs.Task;
        }
    }
}