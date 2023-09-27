using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;
using WeatherSampleOne.Services.Contract.Common;

namespace WeatherSampleOne.ViewModels
{
	public class WeatherSearchViewModel : BaseViewModel
	{
        IWeatherService weatherService;

        public WeatherRequest TheWeatherRequest = new WeatherRequest();

        public WeatherResponse TheWeatherResponse = new WeatherResponse();

        public WeatherSearchViewModel(IWeatherService weatherService)
		{
            this.weatherService = weatherService;
        }

        public bool ValidateCity()
        {
            Errors.Clear();

            if (TheWeatherRequest == null || string.IsNullOrEmpty(TheWeatherRequest?.City) || string.IsNullOrWhiteSpace(TheWeatherRequest?.City))
            {
                Errors.Add(nameof(TheWeatherRequest.City), "City is required.");
            }

            return !HasErrors;
        }

        public bool ValidateCoordinates()
        {
            Errors.Clear();

            if (TheWeatherRequest == null || TheWeatherRequest.Latitude == null)
            {
                Errors.Add(nameof(TheWeatherRequest.City), "Latitude is required.");
            }
            if (TheWeatherRequest == null || TheWeatherRequest.Longitude == null)
            {
                Errors.Add(nameof(TheWeatherRequest.City), "Longitude is required.");
            }

            return !HasErrors;
        }

        public Task GetWeatherAsync(Action<WeatherResponse> onSuccess, Action<ApiError> onError)
        {
            return weatherService.GetWeatherAsync(TheWeatherRequest, response =>
            {
                TheWeatherResponse = response;
                onSuccess?.Invoke(response);
            }, onError);
        }
    }
}

