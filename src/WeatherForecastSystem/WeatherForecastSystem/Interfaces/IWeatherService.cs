using System;
using WeatherForecastSystem.DTOs;

namespace WeatherForecastSystem.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherResponseAsync(LocationResponse locationResponse);
    }
}

