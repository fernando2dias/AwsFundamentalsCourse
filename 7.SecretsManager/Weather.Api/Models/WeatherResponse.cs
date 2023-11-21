using System.Text.Json.Serialization;

namespace Weather.Api.Models;

public class WeatherResponse
{
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("weather")]
    public required List<WeatherDescription> WeatherDescription { get; init; }

    [JsonPropertyName("base")]
    public required string Base { get; init; }

    [JsonPropertyName("visibility")]
    public required long Visibility { get; init; }

    [JsonPropertyName("timezone")]
    public required long Timezone { get; init; }
}


