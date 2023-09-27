using System;
using WeatherSampleOne.Services.Contract.Common;
using Xamarin.Essentials;

namespace WeatherSampleOne.Services.Implementation.Common
{
    public class KeyValueStore : IKeyValueStore
    {
        public string Get(string key, string defaultValue = "")
        {
            return Preferences.Get(key, defaultValue);
        }

        public void Remove(string key)
        {
            Preferences.Remove(key);
        }

        public void Set(string key, string value = "")
        {
            Preferences.Set(key, value);
        }
    }
}

