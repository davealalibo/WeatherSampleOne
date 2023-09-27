using System;
using Autofac;

namespace WeatherSampleOne.ViewModels
{
    public static class ServiceProvider
    {
        public static IContainer Container { get; set; }
        public static T Resolve<T>() => Container.Resolve<T>();
    }
}
