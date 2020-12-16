using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Weather.Logic.Interfaces;
using Weather.Models;

namespace Weather.Logic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly string url = "https://api.openweathermap.org/data/2.5/weather/";

        private readonly string key = "&appid=d8cb0f95b2e2ff51c7b576fed8bbbdd7";

        public async Task<string> GetWeather(string city)
        {            
            string jsonWeather = await GetJaysonFromUrl(city);
            WetherModel wether = Deserialize(jsonWeather);
            float celsius = ConvertKelvinToCelsius(wether.main.temp);
            wether.main.temp = celsius;
            string weatherJson = Serialize(wether.main);
            return weatherJson;
        }

        public async Task<string> GetJaysonFromUrl(string city)
        {
            string urlWithParam = url + "?q=" + city + key;

            using (HttpClient http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(urlWithParam);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        public string Serialize<T>(T wether) => 
            JsonConvert.SerializeObject(wether);

        public WetherModel Deserialize(string jsonWether) =>
            JsonConvert.DeserializeObject<WetherModel>(jsonWether);

        public float ConvertKelvinToCelsius(float tempKelvin) =>
            (float)Math.Round(tempKelvin - 273f, 1);
        
    }
}
