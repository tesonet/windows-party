using System;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using NLog;
using Tesonet.Client.Helpers;
using Tesonet.Client.Services;
using Tesonet.Client.Services.MessengerService;
using Tesonet.Client.Services.NavigationService;
using Tesonet.ServerProxy.Services.AuthorizationService;
using Tesonet.ServerProxy.Services.RequestProvider;
using Tesonet.ServerProxy.Services.ServersService;

namespace Tesonet.Client.ViewModels.Base
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<NavigationToolBarViewModel>();
            SimpleIoc.Default.Register<ServersPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<ErrorPageViewModel>();

            //services
            SimpleIoc.Default.Register<ISettings, Settings>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IAuthorizationService, AuthorizarionService>();
            SimpleIoc.Default.Register<IRequestProvider, RequestProvider>();
            SimpleIoc.Default.Register<IServersService, ServersService>();
            SimpleIoc.Default.Register<IViewModelProvider, ViewModelProvider>();
            SimpleIoc.Default.Register<IMessengerService, MessengerService>();
            SimpleIoc.Default.Register<ILogger>(() => LogManager.GetLogger(typeof(ViewModelLocator).FullName));
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public LoginViewModel LoginViewModel
        {
            get
            {
                SimpleIoc.Default.Unregister<LoginViewModel>();
                SimpleIoc.Default.Register<LoginViewModel>();
                return ServiceLocator.Current.GetInstance<LoginViewModel>(Guid.NewGuid().ToString());
            }
        }
        public MainPageViewModel MainPageViewModel => ServiceLocator.Current.GetInstance<MainPageViewModel>();
        public NavigationToolBarViewModel NavigationToolBarViewModel => ServiceLocator.Current.GetInstance<NavigationToolBarViewModel>();
        public ServersPageViewModel ServersPageViewModel => ServiceLocator.Current.GetInstance<ServersPageViewModel>();
        public SettingsPageViewModel SettingsPageViewModel => ServiceLocator.Current.GetInstance<SettingsPageViewModel>();
        public ErrorPageViewModel ErrorPageViewModel => ServiceLocator.Current.GetInstance<ErrorPageViewModel>();

        public INavigationService NavigationService => ServiceLocator.Current.GetInstance<INavigationService>();
        public ILogger Logger => ServiceLocator.Current.GetInstance<ILogger>();
    }
}