// ReSharper disable InconsistentNaming
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
            public string[][] grid;
        }
    }
}