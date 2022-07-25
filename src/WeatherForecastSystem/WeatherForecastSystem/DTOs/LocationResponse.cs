using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherForecastSystem.DTOs
{
    public class LocationResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("lat")]
        public decimal Latitude { get; set; }
        [JsonPropertyName("lon")]
        public decimal Longitutde { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }
}

