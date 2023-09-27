using System;
namespace WeatherSampleOne.Domain
{
	public class WeatherRequest
	{
		public string City { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
	}
}

