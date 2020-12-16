using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Logic.Interfaces;
using Weather.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;

namespace Weather.Logic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory clientFactory;

        public WeatherService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<TemperatureModel> Get(string city)
        {
            var weather = await GetJaysonFromUrl(city);
            double celsius = ConvertKelvinToCelsius(weather.main.temp);
            weather.main.temp = celsius; 
            return weather.main;
        }

        public async Task<WeatherModel> GetJaysonFromUrl(string city)
        {
            var client = clientFactory.CreateClient("api.openweathermap.org");

            var requestHeader = client.DefaultRequestHeaders.First();
            string key = requestHeader.Key;
            string param = requestHeader.Value.First();


            Dictionary<string, string> query = new Dictionary<string, string> 
            {
                [key] = param,
                ["q"] = city,
            };

            string baseAdress = client.BaseAddress.AbsoluteUri;            

            var weather = await client.GetFromJsonAsync(QueryHelpers.AddQueryString(baseAdress, query), typeof(WeatherModel), default);

            return weather as WeatherModel;
        }

        private double ConvertKelvinToCelsius(double tempKelvin) =>
            Math.Round(tempKelvin - 273, 1);
        
    }
}
