using System;
namespace WeatherForecastTests.TestUtils.Shared
{
    public static class TestConfigurationHelper
    {
        public static IConfiguration SharedTestConfiguration()
        {
            var testSettings = new Dictionary<string, string>
            {
                {"OpenWeatherApi:APIKEY", "TestKey"},
                {"OpenWeatherApi:ClientName","OpenWeatherApi" }
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(testSettings)
                .Build();
        }
    }
}

