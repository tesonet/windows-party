using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WindowsParty.Business;
using WindowsParty.Logging;
using WindowsParty.Services;
using WindowsParty.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WindowsParty
{
	public class AppBootstrapper : BootstrapperBase
	{
		public AppBootstrapper() => Initialize();

		private readonly SimpleContainer _container = new SimpleContainer();

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			var settings = new Dictionary<string, object>{
				{ "Title", "Windows party" },
				{ "ResizeMode", ResizeMode.CanMinimize },
				{ "Height", 780 },
				{ "Width", 1460 }
			};

			DisplayRootViewFor<ShellViewModel>(settings);
		}

		protected override void Configure()
		{
			_container.Singleton<IWindowManager, WindowManager>();
			_container.Singleton<IEventAggregator, EventAggregator>();
			_container.Singleton<LoginViewModel>();
			_container.Singleton<ServersViewModel>();
			_container.Singleton<ShellViewModel>();
			_container.Singleton<IServersService, ServersService>();
			_container.PerRequest<TesonetApiClient>();

			// Separate loggers for caliburn log messages and project log messages.
			LogManager.GetLog = type => new NLogLogger("caliburnLogger");
			_container.Handler<ILogger>(c => new NLogLogger("logger"));
		}

		protected override object GetInstance(Type serviceType, string key) => _container.GetInstance(serviceType, key);

		protected override IEnumerable<object> GetAllInstances(Type serviceType) => _container.GetAllInstances(serviceType);

		protected override void BuildUp(object instance) => _container.BuildUp(instance);
	}
}