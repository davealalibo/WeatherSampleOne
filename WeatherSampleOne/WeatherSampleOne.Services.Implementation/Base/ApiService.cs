using System;
namespace WeatherSampleOne.Services.Implementation.Base
{
    public abstract class ApiService
    {
        protected static RestClient ApiClient { get; set; } = new RestClient();
    }
}

