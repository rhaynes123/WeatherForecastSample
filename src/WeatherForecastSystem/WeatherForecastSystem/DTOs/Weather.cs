using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace WeatherForecastSystem.DTOs
{
    public record Weather
    {
        [JsonPropertyName("id")]
        public int? Id { get; init; }
        [JsonPropertyName("main")]
        public string? Main { get; init; }
        [JsonPropertyName("description")]
        public string? Description { get; init; }
        [JsonPropertyName("icon")]
        public string? Icon { get; init; }
    }
}

