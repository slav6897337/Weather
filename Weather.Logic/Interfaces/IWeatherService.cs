using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Logic.Interfaces
{
    public interface IWeatherService
    {
        Task<TemperatureModel> Get(string city);
    }
}
