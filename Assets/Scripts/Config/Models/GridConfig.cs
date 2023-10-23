namespace Config.Models
{
    [System.Serializable]
    public class GridConfig
    {
        public GridConfiguration[] Grids;

        [System.Serializable]
        public class GridConfiguration
        {
            public int Id;
            public string[][] Grid;
        }
    }
}