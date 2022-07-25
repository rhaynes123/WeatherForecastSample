using System;
namespace WeatherForecastSystem.DTOs
{
    public record LocationRequest(string? City, string? State, string? ZipCode);
}

