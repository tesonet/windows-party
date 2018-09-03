using Caliburn.Micro;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using WindowsPartyApp.Api;
using WindowsPartyApp.Login;
using WindowsPartyApp.Model;
using WindowsPartyApp.Servers;
using WindowsPartyApp.Shell;

namespace WindowsPartyApp.Start
{
    public class AppBootstrapper : BootstrapperBase
    {
        private static readonly Container container = new Container();

        public AppBootstrapper()
        {
            LogManager.GetLog = type => new NLogLogger(type);
            Initialize();

        }

        protected override void Configure()
        {
            container.Register<IWindowManager, WindowManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();

            //App specific
            container.Register<AuthToken, AuthToken>();
            container.Register<ILoginValidator, LoginValidator>();
            container.Register<ShellViewModel, ShellViewModel>();
            container.Register<LoginViewModel, LoginViewModel>();
            container.Register<ServersViewModel, ServersViewModel>();
            container.Register<IApi, TesonetApi>();
            
            container.Verify();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            IServiceProvider provider = container;
            var collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = (IEnumerable<object>)provider.GetService(collectionType);
            return services ?? Enumerable.Empty<object>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                Assembly.GetExecutingAssembly()
            };
        }

        protected override void BuildUp(object instance)
        {
            var registration = container.GetRegistration(instance.GetType(), true);
            registration.Registration.InitializeInstance(instance);
        }
    }
}
