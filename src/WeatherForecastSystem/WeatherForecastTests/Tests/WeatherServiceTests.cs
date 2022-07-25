using System;
namespace WeatherForecastTests.Tests
{
    public class WeatherServiceTests
    {
        #region Resource Links
        // https://www.youtube.com/watch?v=7OFZZAHGv9o
        // https://github.com/richardszalay/mockhttp
        // https://www.thecodebuzz.com/unit-test-mock-httpclientfactory-moq-net-core/
        #endregion Resource Links
        private const string BaseUrl = "http://api.openweathermap.org/";
        private const string ClientName = "OpenWeatherApi";
        private readonly IConfiguration configuration;
        private readonly Mock<IHttpClientFactory> _mockClientFactory = new();
        private readonly MockHttpMessageHandler _mockHttpMessageHandler = new();
        public WeatherServiceTests()
        {
            configuration = TestConfigurationHelper.SharedTestConfiguration();
        }
        [Fact]
        public async Task ShouldReturnWeatherDataAsync()
        {
            //Arrange
            string folder = Environment.CurrentDirectory;
            string filePath = Path.Combine(folder, $@"TestFiles/WeatherForSampleCityTestFile.json");
            string jsonString = File.ReadAllText(filePath);
            string queryString = new WeatherQueryStringBuilder($"{BaseUrl}data/2.5/weather?")
                .AddLatAndLon(latitude: 42.3314m, longitude: -83.0458m)
                .AddLimit(10)
                .AddApiKey("TestKey")
                .AddUnitsOfMeasure("imperial")
                .Build();
            _mockHttpMessageHandler.When(queryString)
                .Respond(HttpStatusCode.OK, "application/json", jsonString);
            _mockClientFactory.Setup(r => r.CreateClient(ClientName)).Returns(new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            }); ;
            //Act
            WeatherService weatherService = new WeatherService(_mockClientFactory.Object, configuration);
            var result = await weatherService.GetWeatherResponseAsync(new LocationResponse()
            {
                Latitude = 42.3314m,
                Longitutde = -83.0458m
            });
            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Weathers);
        }
    }
}

