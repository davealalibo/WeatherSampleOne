using System;
namespace WeatherSampleOne.Services.Contract.Common
{
    public interface ITelephony
    {
        bool IsNetorkAvailable(object context);
        string GetDeviceId(object context);
    }
}

