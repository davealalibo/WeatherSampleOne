using System;
namespace WeatherSampleOne.Domain.Network
{
	public class ApiRequest
	{
        public string Path { get; set; }
        public object Payload { get; set; }
        public object Headers { get; set; }
        public object Cookies { get; set; }
        public object QueryParams { get; set; }
    }
}

