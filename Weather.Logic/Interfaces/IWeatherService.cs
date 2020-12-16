using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Logic.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeather(string city);
    }
}
