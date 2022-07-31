using Demo.Weather.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));

            _logger.LogInformation($"Instantiated {nameof(WeatherForecastController)} class.");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"Invoked {nameof(WeatherForecastController)}.{nameof(Get)} method.");

            var results = await _weatherService.GeForecastAsync();

            if (results?.Length == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }
    }
}