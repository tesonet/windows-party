/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Tesonet.Windows_party"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Tesonet.Windows_party.Services;
using Tesonet.Windows_party.Services.Implementations;
using Tesonet.Windows_party.Services.Interfaces;

namespace Tesonet.Windows_party.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);    
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<ServerListViewModel>();
            SimpleIoc.Default.Register<IApiService, ApiService>();
            SimpleIoc.Default.Register<ILoggerService, FileLoggerService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
        }

        public LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>(Guid.NewGuid().ToString());

        public ServerListViewModel ServerListViewModel => ServiceLocator.Current.GetInstance<ServerListViewModel>(Guid.NewGuid().ToString());

        public INavigationService NavigationService => ServiceLocator.Current.GetInstance<INavigationService>();

        public static void Cleanup()
        {
            
        }
    }
}