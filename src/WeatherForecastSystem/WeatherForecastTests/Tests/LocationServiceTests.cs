using System;
namespace WeatherForecastTests.Tests
{
    public class LocationServiceTests
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
        public LocationServiceTests()
        {
            configuration = TestConfigurationHelper.SharedTestConfiguration();
        }

        [Fact]
        public async Task ShouldReturnLocationDataAsync()
        {
            //Arrange
            int locationCount = 5;
            string folder = Environment.CurrentDirectory;
            string filePath = Path.Combine(folder,$@"TestFiles/LocationSampleCityTestFile.json") ;
            string jsonString = File.ReadAllText(filePath);
            _mockHttpMessageHandler.When($"{BaseUrl}geo/1.0/direct?q=Detroit,&limit=10&appid=TestKey")
                .Respond(HttpStatusCode.OK, "application/json", jsonString);
            _mockClientFactory.Setup(r => r.CreateClient(ClientName)).Returns(new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            }); ;
            //Act
            var locationService = new LocationService(_mockClientFactory.Object, configuration);
            var result = await locationService.GetLocationsResponseAsync(new LocationRequest("Detroit", string.Empty, string.Empty));
            //Assert
            Assert.Equal(locationCount, result.LocationResponses.Count());
        }
    }
}

