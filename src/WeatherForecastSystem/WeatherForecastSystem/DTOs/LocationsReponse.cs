using System;
namespace WeatherForecastSystem.DTOs
{
    public record LocationsReponse(IEnumerable<LocationResponse>? LocationResponses);
}

