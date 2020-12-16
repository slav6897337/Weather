using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Logic.Interfaces;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {        

        private readonly IWeatherService _wetherService;

        public WeatherForecastController(IWeatherService wetherService)
        {
            _wetherService = wetherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                string result =  await _wetherService.GetWeather(city);
                return Ok(result);
            }

            return Ok();
        }
    }
}
