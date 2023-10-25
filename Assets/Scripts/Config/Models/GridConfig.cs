using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Config.Models
{
    [Serializable]
    public class GridConfig
    {
        [JsonProperty("grid")] public List<List<string>> Grid;
    }
}