﻿using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeatherForecastSystem.DTOs;
using WeatherForecastSystem.Interfaces;
using System.Collections.Generic;
using WeatherForecastSystem.Models;

namespace WeatherForecastSystem.Services
{
    public class LocationService: ILocationService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly string ApiKey;
        public LocationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _client = httpClientFactory.CreateClient(_configuration["OpenWeatherApi:ClientName"]);
            if(_client is null)
            {
                ArgumentNullException.ThrowIfNull(_client);
            }
            if (string.IsNullOrWhiteSpace(_configuration["OpenWeatherApi:APIKEY"]))
            {
                throw new ArgumentException("ApiKey Configuration Missing");
            }
            ApiKey = _configuration["OpenWeatherApi:APIKEY"];
        }

        public async Task<LocationsReponse> GetLocationsResponseAsync(LocationRequest locationRequest)
        {
            WeatherQueryStringBuilder queryString = new WeatherQueryStringBuilder("geo/1.0/direct?q=")
               .AddCity(locationRequest.City)
               .AddState(locationRequest.State)
                .AddLimit(10)
               .AddApiKey(ApiKey: ApiKey);
            HttpResponseMessage response = await _client.GetAsync(queryString.Build());
            if(response is null)
            {
                ArgumentNullException.ThrowIfNull(response);
            }
            if(!response.IsSuccessStatusCode || response.Content is null)
            {
                throw new Exception($"Endpoint return bad status code {response.StatusCode}");
            }
            IEnumerable<LocationResponse> locations = await response.Content.ReadFromJsonAsync<IEnumerable<LocationResponse>>();
            return new LocationsReponse(locations);
        }
    }
}

