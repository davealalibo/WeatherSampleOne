using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherSampleOne.Config;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;
using WeatherSampleOne.Services.Contract.Common;
using WeatherSampleOne.Services.Implementation.Base;

namespace WeatherSampleOne.Services.Implementation
{
	public class WeatherService : ApiService, IWeatherService
    {
        //private readonly HttpClient httpClient;

        //public WeatherService(HttpClient httpClient)
        //{
        //    this.httpClient = httpClient;
        //}

        //public async Task<WeatherResponse> GetWeatherAsync(double latitude, double longitude)
        //{
        //    try
        //    {
        //        var apiKey = Constants.WEATHER_API_KEY; // Replace with your OpenWeatherMap API key
        //        var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";

        //        var response = await httpClient.GetAsync(apiUrl);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(content);
        //            return weatherData;
        //        }
        //        else
        //        {
        //            throw new Exception($"API request failed with status code: {response.StatusCode}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"API request failed: {ex.Message}");
        //    }
        //}

        public Task GetWeatherAsync(WeatherRequest weatherRequest, Action<WeatherResponse> onSuccess, Action<ApiError> onError)
        {
            return ApiClient.Get(new ApiRequest
            {
                Path = string.Format(Urls.GET_CURRENT_WEATHER, weatherRequest.Latitude, weatherRequest.Longitude, Constants.WEATHER_API_KEY),
            }, onSuccess, onError);
        }
    }
}

