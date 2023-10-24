// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Config.Models
{
    [System.Serializable]
    public class GridConfig
    {
        public GridConfiguration[] grids;

        [System.Serializable]
        public class GridConfiguration
        {
            public int id;
            [JsonProperty("grid")]
            public List<List<string>> grid;
        }
    }
}