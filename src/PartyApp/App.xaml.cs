using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using PartyApp.Log;
using PartyApp.Utilities;
using Prism.Mvvm;

namespace PartyApp
{
	public partial class App : Application
	{
		private IAppLogger _appLogger;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var container = new UnityContainer().
				ConfigureServices().
				ConfigureLog(EnableInfoLevelLogging(e));

			_appLogger = container.Resolve<IAppLogger>();

			ViewModelLocationProvider.SetDefaultViewModelFactory(
				(view, viewModelType) => container.Resolve(viewModelType));

			TaskScheduler.UnobservedTaskException 
				+= TaskScheduler_UnobservedTaskException;
		}

		private void TaskScheduler_UnobservedTaskException(
			object sender, UnobservedTaskExceptionEventArgs e)
		{
			_appLogger.WriteException(e.Exception);
		}

		private void Application_DispatcherUnhandledException(
			object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			_appLogger.WriteException(e.Exception);

			//don't let app to shutdown
			//it's state should not be corrupted
			e.Handled = true;

			MessageDialogsHelper.ShowError(
				PartyApp.Properties.Resources.UnexpectedError);
		}

		private bool EnableInfoLevelLogging(StartupEventArgs e)
		{
			var args = e.Args.Select(
				a => a.Trim().TrimStart(new[] { '-', '-' })).FirstOrDefault();

			return !string.IsNullOrEmpty(args) &&
				args.Equals("Log", StringComparison.OrdinalIgnoreCase);
		}
	}
}
