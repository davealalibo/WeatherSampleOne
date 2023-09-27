using System;
namespace WeatherSampleOne.Config
{
    public static class Urls
    {
        public const string WEATHER_BASE_URL = "https://api.openweathermap.org/data/2.5/";

        #region APICALLS
        public const string GET_CURRENT_WEATHER = WEATHER_BASE_URL + "weather?lat={0}&lon={1}&appid={2}";
        #endregion

    }
}

