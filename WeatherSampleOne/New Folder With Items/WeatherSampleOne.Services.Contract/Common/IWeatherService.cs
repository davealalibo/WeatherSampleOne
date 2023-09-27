using System;
using WeatherSampleOne.Domain;

namespace WeatherSampleOne.Services.Contract.Common
{
	public interface IWeatherService
	{
        Task<WeatherResponse> GetWeatherAsync(double latitude, double longitude);
    }
}

