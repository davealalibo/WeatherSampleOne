﻿using System;
using System.Threading.Tasks;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;

namespace WeatherSampleOne.Services.Contract.Common
{
	public interface IWeatherService
	{
        Task GetWeatherAsync(WeatherRequest weatherRequest, Action<WeatherResponse> onSuccess, Action<ApiError> onError);
    }
}

