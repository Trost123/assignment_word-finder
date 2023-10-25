using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Config.Services
{
    [UsedImplicitly]
    public class ConfigurationLoader
    {
        public static async Task<T> LoadConfigAsync<T>(string path)
        {
            try
            {
                // Read the file asynchronously
                using var streamReader = new StreamReader(path);
                var jsonText = await streamReader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load config from path: {path}, Exception: {ex.Message}");
                return default; // Returns null for reference types, zero for numeric types, etc.
            }
        }
    }
}