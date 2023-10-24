using System.Threading.Tasks;
using Config.Interfaces;
using Config.Models;
using JetBrains.Annotations;
using Newtonsoft.Json;
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

        public Task<GridConfig> LoadConfigurationAsync(string path)
        {
            var tcs = new TaskCompletionSource<GridConfig>();

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
                        var jsonText = JsonConvert.SerializeObject(gridConfig);
                        Debug.Log(jsonText);
                    }

                    // Further processing of gridConfig if necessary...

                    tcs.SetResult(gridConfig);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return tcs.Task;
        }
    }
}