using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weather.Logic.Interfaces;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {        

        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                var result = await _weatherService.Get(city);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound(); 
            }

            return Ok();
        }
    }
}
