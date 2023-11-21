namespace Weather.Api.Models;

public class OpenWeatherApiSettings
{
    public const string Key = "OpenWeatherMapApi";

    public required string ApiKey { get; set; }
}
