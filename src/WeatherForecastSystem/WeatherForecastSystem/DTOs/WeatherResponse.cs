using System;
using System.Text.Json.Serialization;

namespace WeatherForecastSystem.DTOs
{
    public record WeatherResponse()
    {
        [JsonPropertyName("main")]
        public Main? Main { get; init; }
        [JsonPropertyName("weather")]
        public IEnumerable<Weather>? Weathers { get; init; }
    }
}

