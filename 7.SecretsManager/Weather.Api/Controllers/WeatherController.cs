using Microsoft.AspNetCore.Mvc;
using Weather.Api.Services;

namespace Weather.Api.Controllers;

[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("weather/{city}")]
    public async Task<IActionResult> GetWeatherForCity(string city)
    {
        var weather = await _weatherService.GetCurrentWeatherAsync(city);
        if (weather is null)
        {
            return NotFound();
        }

        return Ok(weather);
    }
}
