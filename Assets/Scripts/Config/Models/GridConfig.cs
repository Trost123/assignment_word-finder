// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Config.Models
{
    [Serializable]
    public class GridConfig
    {
        public GridConfiguration[] grids;

        [Serializable]
        public class GridConfiguration
        {
            public int id;

            [JsonProperty("grid")] public List<List<string>> grid;
        }
    }
}