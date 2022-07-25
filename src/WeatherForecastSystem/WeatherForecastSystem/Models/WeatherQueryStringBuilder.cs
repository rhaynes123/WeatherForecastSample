using System;
using System.Text;
namespace WeatherForecastSystem.Models
{
    public class WeatherQueryStringBuilder
    {
        private StringBuilder QueryString { get; set; } 
        public WeatherQueryStringBuilder(string baseQuery)
        {
            if (string.IsNullOrWhiteSpace(baseQuery))
            {
                ArgumentNullException.ThrowIfNull(baseQuery);
            }
            QueryString = new StringBuilder(baseQuery);
        }
       
        public WeatherQueryStringBuilder AddCity(string City)
        {
            if (!string.IsNullOrWhiteSpace(City))
            {
                QueryString.Append($"{City},");
            }
            return this;
        }
        public WeatherQueryStringBuilder AddState(string State)
        {
            if (!string.IsNullOrWhiteSpace(State))
            {
                QueryString.Append($"{State},");
            }
            return this;
        }
        public WeatherQueryStringBuilder AddLatAndLon(decimal latitude, decimal longitude)
        {
            QueryString.Append($"lat={latitude}");
            QueryString.Append($"&lon={longitude}");
            return this;
        }
        public WeatherQueryStringBuilder AddLimit(int limit)
        {
            QueryString.Append($"&limit={limit}");
            return this;
        }
        public WeatherQueryStringBuilder AddUnitsOfMeasure(string unitsOfMeasure)
        {
            QueryString.Append($"&units={unitsOfMeasure}");
            return this;
        }
        public WeatherQueryStringBuilder AddApiKey(string ApiKey)
        {
            QueryString.Append($"&appid={ApiKey}");
            return this;
        }
        public string Build()
        {
            string query = QueryString.ToString();
            if (string.IsNullOrWhiteSpace(query))
            {
                ArgumentNullException.ThrowIfNull(query);
            }
            return query;
        }
    }
}

