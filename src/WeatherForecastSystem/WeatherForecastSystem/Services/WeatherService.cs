using System;
using System.Text;
using WeatherForecastSystem.DTOs;
using WeatherForecastSystem.Interfaces;
using WeatherForecastSystem.Models;
namespace WeatherForecastSystem.Services
{
    public class WeatherService: IWeatherService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly string ApiKey;
        public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _client = httpClientFactory.CreateClient(_configuration["OpenWeatherApi:ClientName"]);
            if(string.IsNullOrWhiteSpace(_configuration["OpenWeatherApi:APIKEY"]))
            {
                throw new ArgumentException("ApiKey Configuration Missing");
            }
            ApiKey = _configuration["OpenWeatherApi:APIKEY"];
        }

        public async Task<WeatherResponse> GetWeatherResponseAsync(LocationResponse locationResponse)
        {
            WeatherQueryStringBuilder querystring = new WeatherQueryStringBuilder("data/2.5/weather?")
                .AddLatAndLon(locationResponse.Latitude, locationResponse.Longitutde)
                .AddLimit(10)
                .AddApiKey(ApiKey: ApiKey)
                .AddUnitsOfMeasure("imperial");
            HttpResponseMessage response = await _client.GetAsync(querystring.Build());
            if (!response.IsSuccessStatusCode || response.Content is null)
            {
                throw new Exception($"Endpoint return bad status code {response.StatusCode}");
            }
            WeatherResponse weatherdata = await response.Content.ReadFromJsonAsync<WeatherResponse>();
            return weatherdata;
        }
    }
}

