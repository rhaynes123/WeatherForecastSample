using System;
using System.Text;
using WeatherForecastSystem.DTOs;
using WeatherForecastSystem.Interfaces;

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
            var queryString = new StringBuilder("data/2.5/weather?");
            queryString.Append($"lat={locationResponse.Latitude}");
            queryString.Append($"&lon={locationResponse.Longitutde}");
            queryString.Append($"&limit=10");
            queryString.Append($"&appid={ApiKey}");
            queryString.Append("&units=imperial");
            HttpResponseMessage response = await _client.GetAsync(queryString.ToString());
            if (!response.IsSuccessStatusCode || response.Content is null)
            {
                throw new Exception($"Endpoint return bad status code {response.StatusCode}");
            }
            WeatherResponse weatherdata = await response.Content.ReadFromJsonAsync<WeatherResponse>();
            return weatherdata;
        }
    }
}

