using System.Threading.Tasks;
using Config.Models;

namespace Config.Interfaces
{
    public interface IConfigReader
    {
        Task<GridConfig> LoadConfig(string path);
    }
}