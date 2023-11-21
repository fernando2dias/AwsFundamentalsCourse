using Weather.Api.Models;

namespace Weather.Api.Services;

public interface IWeatherService
{
    Task<WeatherResponse?> GetCurrentWeatherAsync(string city);
}
