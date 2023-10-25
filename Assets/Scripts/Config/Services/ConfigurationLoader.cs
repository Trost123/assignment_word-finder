using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Config.Services
{
    [UsedImplicitly]
    public class ConfigurationLoader
    {
        public async Task<T> LoadConfigAsync<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(path);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return JsonConvert.DeserializeObject<T>(handle.Result.text);
            }

            Debug.LogError($"Failed to load config from path: {path}, Status: {handle.Status}");
            return default;  // Returns null for reference types, zero for numeric types, etc.
        }
    }
}