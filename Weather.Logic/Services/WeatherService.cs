using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Logic.Interfaces;
using Weather.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Weather.Configuration.Options;

namespace Weather.Logic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConnectionStringsOptions options;

        public WeatherService(IHttpClientFactory clientFactory, IOptions<ConnectionStringsOptions> options)
        {
            this.clientFactory = clientFactory;
            this.options = options.Value;
        }
        public async Task<TemperatureModel> Get(string city)
        {
            var weather = await GetJaysonFromUrl(city);
            if (weather == null)
                return null;
            double celsius = ConvertKelvinToCelsius(weather.main.temp);
            weather.main.temp = celsius;
            return weather.main;
        }

        private async Task<WeatherModel> GetJaysonFromUrl(string city)
        {
            var client = clientFactory.CreateClient("api.openweathermap.org");

            Dictionary<string, string> query = new Dictionary<string, string>
            {
                [options.HeaderName] = options.HeaderValue,
                [options.HeaderNameCity] = city,
            };

            var url = QueryHelpers.AddQueryString(client.BaseAddress.AbsoluteUri, query);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var weather = await client.GetFromJsonAsync(url,
                                                            typeof(WeatherModel),
                                                            default);

                return weather as WeatherModel;
            }
            else
                return null;
        }

        private double ConvertKelvinToCelsius(double tempKelvin) =>
            Math.Round(tempKelvin - 273, 1);

    }
}
