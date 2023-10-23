using System.Threading.Tasks;
using Config.Interfaces;
using Config.Models;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Config.Services
{
    [UsedImplicitly]
    public class JsonConfigReader : IConfigReader
    {
        public async Task<GridConfig> LoadConfig(string path)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(path);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return JsonUtility.FromJson<GridConfig>(handle.Result.text);
            }
            else
            {
                Debug.LogError($"Failed to load config from path: {path}, Status: {handle.Status}");
                return null;
            }
        }
    }
}