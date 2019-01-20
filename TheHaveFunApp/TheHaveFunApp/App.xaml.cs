using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using CommonServiceLocator;
using Prism;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using TheHaveFunApp.ViewModels;
using TheHaveFunApp.Views;

namespace TheHaveFunApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {



        public override void Initialize()
        {
            base.Initialize();

            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.AddToRegion("MainRegion", this.Container.Resolve<LoginPage>());
        }


        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
