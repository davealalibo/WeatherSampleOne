using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherSampleOne.Domain
{
    public class WeatherResponse
    {
        public Coordinates Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public MainData Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Clouds Clouds { get; set; }
        public long Dt { get; set; }
        public SysData Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }

    public class Coordinates
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public int GrndLevel { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
    }

    public class Rain
    {
        [JsonProperty("1h")]
        public double Rain1h { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class SysData
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }

}

