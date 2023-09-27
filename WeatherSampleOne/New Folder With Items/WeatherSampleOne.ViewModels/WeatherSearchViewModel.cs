using System;
namespace WeatherSampleOne.ViewModels
{
	public class WeatherSearchViewModel : BaseViewModel
	{
        public WeatherRequest weatherRequest;
		public WeatherSearchViewModel()
		{
        }

        private bool ValidateCity()
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                ErrorMessage = "City is required.";
                return false;
            }

            // Additional city validation logic can be added here if needed.

            return true;
        }

        private bool ValidateCoordinates()
        {
            if (Coordinates == null)
            {
                ErrorMessage = "Coordinates are required.";
                return false;
            }

            if (string.IsNullOrEmpty(Coordinates.Latitude) || string.IsNullOrEmpty(Coordinates.Longitude))
            {
                ErrorMessage = "Latitude and longitude are required.";
                return false;
            }

            // Additional validation logic for latitude and longitude can be added here if needed.

            return true;
        }
    }
}

