using System;
namespace WeatherSampleOne.Services.Contract.Common
{
    public interface IKeyValueStore
    {
        void Set(string key, string value = "");
        string Get(string key, string defaultValue = "");
        void Remove(string key);
    }
}

