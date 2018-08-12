using System;
using System.Windows;
using System.Windows.Navigation;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs args)
        {
            var locator = new ViewModelLocator();
            locator.Logger.Error(args.Exception, "Unhandled exception");

            if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsActive)
            {
                //todo: show custom window
                locator.NavigationService.NavigateToErrorPageAsync(new ErrorPageNavigationData
                {
                    ErrorTitle = Client.Properties.Resources.UnhandledException,
                    ErrorMessage = args.Exception.Message,
                    NavigatedFromPage = null
                });

                args.Handled = true;
            }
            else
            {
                MessageBox.Show(args.Exception.Message, "Unhandled exception occured");
            }
        }
    }
}
