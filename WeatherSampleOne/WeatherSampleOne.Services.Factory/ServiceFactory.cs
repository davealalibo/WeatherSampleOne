using System;
using System.Buffers.Text;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Autofac;
using WeatherSampleOne.Services.Contract.Common;
using WeatherSampleOne.Services.Implementation.Base;
using WeatherSampleOne.Services.Implementation.Common;
using WeatherSampleOne.Services.Mock;
using WeatherSampleOne.ViewModels;

namespace WeatherSampleOne.Services.Factory
{
    public static class ServiceFactory
    {

        static ServiceFactory()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }

        public static IContainer Container { get; set; }
        public static bool ReIssueTokenOnError = true;
        public static T Resolve<T>() => Container.Resolve<T>();
        private static ContainerBuilder builder;
        public static void Create(Action<ContainerBuilder> builderConfig)
        {
            builder = new ContainerBuilder();
#if DEBUG

            //Globals.IsMockData = true;
            //***************************************
            // Register mock services here
            builder.RegisterAssemblyTypes(typeof(MockWeatherService).Assembly)
                .Where(t => t.Name.Contains("Service")
                        && t.IsClass && !t.IsAbstract)
                .AsImplementedInterfaces();
#else
            //*******************************************
            //Register the concrete services here
            builder.RegisterAssemblyTypes(typeof(ApiService).Assembly)
                .Where(t => t.Name.EndsWith("Service")
                && t.IsClass && !t.IsAbstract)
                .AsImplementedInterfaces();


#endif
            //*******************************************
            //Register services without mocks (same implementation for all configurations) here
            builder.RegisterType<KeyValueStore>().As<IKeyValueStore>();
            builder.RegisterType<JsonService>().As<IJsonService>();


            //***************************************
            //Register BaseViewModel here if you will make all view models inherit from it
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BaseViewModel)))
                .Where(t => t.IsSubclassOf(typeof(BaseViewModel)))
                .AsSelf().SingleInstance();

            //Alternatively, register each of the view model or any view model that did not inhereit from BaseVIewModel
            //builder.RegisterType<WeatherSearchViewModel>().SingleInstance();

            //*******************************************
            //run configurations/registrations specified by the calling platform
            builderConfig?.Invoke(builder);
            Container = builder?.Build();
            ViewModels.ServiceProvider.Container = Container;
        }
    }
}

