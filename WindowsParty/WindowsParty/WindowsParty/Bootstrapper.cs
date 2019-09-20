using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using WindowsParty.Handlers;
using WindowsParty.Handlers.Contracts;
using WindowsParty.ViewModels;

namespace WindowsParty
{
	public class Bootstrapper : BootstrapperBase
	{
		#region Properties

		private SimpleContainer container;

		#endregion

		#region Constructors

		public Bootstrapper()
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		protected override void Configure()
		{
			container = new SimpleContainer();
			container.Instance(container);

			container
				.Singleton<IWindowManager, WindowManager>()
				.Singleton<IEventAggregator, EventAggregator>()
				.Singleton<ILoginHandler, LoginHandler>()
				.Singleton<IServersHandler, ServersHandler>();

			container
			   .PerRequest<ShellViewModel>()
			   .PerRequest<LoginViewModel>()
			   .PerRequest<ServerListViewModel>();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = container.GetInstance(service, key);
			if (instance != null)
				return instance;
			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}

		protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			var ex = e.Exception;
			while (ex.InnerException != null) ex = ex.InnerException;
			MessageBox.Show(ex.Message, "An error as occurred", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		#endregion
	}
}
