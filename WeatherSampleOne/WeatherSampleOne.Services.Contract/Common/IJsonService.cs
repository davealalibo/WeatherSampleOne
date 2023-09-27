using System;
namespace WeatherSampleOne.Services.Contract.Common
{
    public interface IJsonService
    {
        string Serialize(object data);
        T Deserialize<T>(string json);
    }
}

