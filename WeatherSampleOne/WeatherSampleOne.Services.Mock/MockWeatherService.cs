using System;
using System.Threading.Tasks;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;
using WeatherSampleOne.Services.Contract.Common;

namespace WeatherSampleOne.Services.Mock
{
    public class MockWeatherService : IWeatherService
    {
        public MockWeatherService()
        {
        }

        public async Task GetWeatherAsync(WeatherRequest weatherRequest, Action<WeatherResponse> onSuccess, Action<ApiError> onError)
        {
            //Simulate an endpoint call delay
            await Task.Delay(200);

            // Sample JSON data
            string jsonData = "{\"coord\":{\"lon\":3.3533,\"lat\":6.4985},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03d\"}],\"base\":\"stations\",\"main\":{\"temp\":305.29,\"feels_like\":312.29,\"temp_min\":305.29,\"temp_max\":305.29,\"pressure\":1010,\"humidity\":70},\"visibility\":10000,\"wind\":{\"speed\":6.69,\"deg\":220},\"clouds\":{\"all\":40},\"dt\":1695825555,\"sys\":{\"type\":1,\"id\":1185,\"country\":\"NG\",\"sunrise\":1695792903,\"sunset\":1695836421},\"timezone\":3600,\"id\":2332459,\"name\":\"Lagos\",\"cod\":200}";

            // Deserialize JSON into the Root object
            WeatherResponse weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherResponse>(jsonData);

            onSuccess?.Invoke(weatherData);

        }
    }
}
