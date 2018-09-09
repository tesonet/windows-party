using WindowsParty.Core.External;
using WindowsParty.UI.Windows.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WindowsParty.Core.External.PlaygroundClient;
using Caliburn.Micro;
using WindowsParty.Core.Services;

namespace WindowsParty.UI.Windows
{
    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            container
                .PerRequest<IPlaygroundClient, PlaygroundClient>()
                .PerRequest<ITokenService, TokenService>()
                .PerRequest<IServerService, ServerService>()
                .PerRequest<LoginViewModel>()
                .PerRequest<ServerListViewModel>()
                .PerRequest<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}