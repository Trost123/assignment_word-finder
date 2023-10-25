using System;
using Newtonsoft.Json;

namespace Config.Models
{
    [Serializable]
    public class AppConfig
    {
        [JsonProperty("incorrect_input_behavior")]
        public IncorrectInputBehaviorConfig IncorrectInputBehavior { get; set; }

        [Serializable]
        public class IncorrectInputBehaviorConfig
        {
            public bool Shake { get; set; }
            public bool Sound { get; set; }
        }
    }
}