using System;
using System.Buffers.Text;
using System.Globalization;
using System.Reflection;
using Autofac;
using WeatherSampleOne.Services.Contract.Common;
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

            Globals.IsMockData = true;
            //***************************************
            // Register mock services here
            builder.RegisterAssemblyTypes(typeof(Mock.Onboarding.MockLoginService).Assembly)
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
            builder.RegisterType<DbService>().As<IDbService>();
            builder.RegisterType<LocationService>().As<ILocationService>();
            builder.RegisterType<KeyValueStore>().As<IKeyValueStore>();
            builder.RegisterType<JsonService>().As<IJsonService>();
            builder.RegisterType<AlatHttpClientFactory>().SingleInstance();
            builder.RegisterType<Authenticator>().As<IAuthenticator>();


            //***************************************
            //Register ViewModels here
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BaseViewModel)))
                .Where(t => t.IsSubclassOf(typeof(BaseViewModel)))
                .AsSelf().SingleInstance();

            //***************************************
            //Register SignalRService here
            builder.RegisterType<SignalRService>().AsSelf().SingleInstance();

            //*******************************************
            //run configurations/registrations specified by the calling platform
            builderConfig?.Invoke(builder);
            Container = builder?.Build();
            RestClient.ClientFactory = Container?.Resolve<AlatHttpClientFactory>();
            ViewModels.ServiceProvider.Container = Container;
        }
    }
}

