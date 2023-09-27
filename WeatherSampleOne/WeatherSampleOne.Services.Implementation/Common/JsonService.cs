using System;
using Newtonsoft.Json;
using WeatherSampleOne.Services.Contract.Common;

namespace WeatherSampleOne.Services.Implementation.Common
{
    public class JsonService : IJsonService
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}

