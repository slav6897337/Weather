using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Logic.Interfaces;
using Weather.Models;
using System.Net.Http.Json;

namespace Weather.Logic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly string url = "https://api.openweathermap.org/data/2.5/weather/";

        private readonly string key = "&appid=d8cb0f95b2e2ff51c7b576fed8bbbdd7";

        public async Task<TemperatureModel> Get(string city)
        {
            var weather = await GetJaysonFromUrl(city);
            double celsius = ConvertKelvinToCelsius(weather.main.temp);
            weather.main.temp = celsius; 
            return weather.main;
        }

        public async Task<WeatherModel> GetJaysonFromUrl(string city)
        {
            string urlWithParam = url + "?q=" + city + key;

            using (HttpClient http = new HttpClient())
            {
                var weather = await http.GetFromJsonAsync(urlWithParam, typeof(WeatherModel), default);
                return weather as WeatherModel;
            }            
        }

        private double ConvertKelvinToCelsius(double tempKelvin) =>
            Math.Round(tempKelvin - 273, 1);
        
    }
}
