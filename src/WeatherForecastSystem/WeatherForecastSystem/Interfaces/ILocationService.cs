using System;
using WeatherForecastSystem.DTOs;

namespace WeatherForecastSystem.Interfaces
{
    public interface ILocationService
    {
        Task<LocationsReponse> GetLocationsResponseAsync(LocationRequest locationRequest);
    }
}

