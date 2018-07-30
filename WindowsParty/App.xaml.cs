using System.Windows;
using Unity;
using Unity.Lifetime;
using WindowsParty.Common.Interfaces;
using WindowsParty.Services;
using WindowsParty.ViewModels;

namespace WindowsParty
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IUnityContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _container = new UnityContainer();

            _container.RegisterType<IAppSettings, AppSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserSessionService, UserSessionService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ILogService, FileNLogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAuthorizationService, AuthorizationService>();
            _container.RegisterType<IServerService, ServerService>();

            _container.RegisterType<ILoginViewModel, LoginViewModel>();
            _container.RegisterType<IServerViewModel, ServerViewModel>();
            _container.RegisterType<IMainViewModel, MainViewModel>(new ContainerControlledLifetimeManager());

            var mainWindow = new MainWindow(_container.Resolve<IMainViewModel>());

            _container.Resolve<ILogService>().Info("Start");

            mainWindow.Show();     
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _container.Resolve<ILogService>().Info("End");
        }
    }
}
