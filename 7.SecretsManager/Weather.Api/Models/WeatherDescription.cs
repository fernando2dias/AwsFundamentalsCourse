using System.Text.Json.Serialization;

namespace Weather.Api.Models;

public class WeatherDescription
{
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("main")]
    public required string Main { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("icon")]
    public required string Icon { get; init; }
}
