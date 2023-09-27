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
        public Task GetWeatherAsync(WeatherRequest weatherRequest, Action<WeatherResponse> onSuccess, Action<ApiError> onError)
        {
            return ApiClient.Get(new ApiRequest
            {
                Path = string.Format(Urls.GET_CURRENT_WEATHER, weatherRequest.Latitude, weatherRequest.Longitude, Constants.WEATHER_API_KEY),
            }, onSuccess, onError);
        }
    }
}

