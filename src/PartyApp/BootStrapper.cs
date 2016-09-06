using Microsoft.Practices.Unity;
using NLog;
using PartyApp.Clients;
using PartyApp.Log;
using PartyApp.Models;
using PartyApp.Services;
using PartyApp.Utilities;
using Prism.Events;

namespace PartyApp
{
	public static class BootStrapper
	{
		public static IUnityContainer ConfigureServices(
			this IUnityContainer container)
		{
			Guard.NotNull(container, nameof(container));
			container.RegisterInstance<IEventAggregator>(new EventAggregator());
			container.RegisterType<IAuthorizer, AuthorizationClient>();
			container.RegisterType<IServersProvider, ServersClient>();
			container.RegisterType<ILoginModel, LoginModel>();
			container.RegisterType<IServersModel, ServersModel>();
			return container;
		}

		public static IUnityContainer ConfigureLog(
			this IUnityContainer container, bool enableInfoLevelLogging = false)
		{
			Guard.NotNull(container, nameof(container));

			//it's recommended to have one logger for each class
			container.RegisterType<ILogger, Logger>(new InjectionFactory(c =>
			{
				return LogManager.GetCurrentClassLogger();
			}));

			container.RegisterType<IAppLogger, NLogLoggerAdapter>(new InjectionFactory(c =>
			{
				var logger = new NLogLoggerAdapter(c.Resolve<ILogger>());
				if (enableInfoLevelLogging)
					logger.Enable();

				return logger;
			}));

			return container;
		}
	}
}
