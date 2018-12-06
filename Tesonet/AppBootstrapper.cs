/// <summary>
/// Bootstrapper file
/// </summary>
namespace Tesonet
{
    using System;
    using Caliburn.Micro;
    using System.Collections.Generic;
    using System.Windows;
    using Tesonet.Services;
    using Tesonet.ViewModels;

    /// <summary>AppBootstrapper App</summary>
    public class AppBootstrapper : Caliburn.Micro.BootstrapperBase
    {
        /// <summary>
        /// The container
        /// </summary>
        private SimpleContainer container = new SimpleContainer();

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        public AppBootstrapper()
        {
            this.Initialize();
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            this.container.Singleton<IEventAggregator, EventAggregator>();
            this.container.PerRequest<MainViewModel, MainViewModel>();
            this.container.PerRequest<LoginViewModel, LoginViewModel>();
            this.container.PerRequest<ServerListViewModel, ServerListViewModel>();
            this.container.Singleton<IWindowManager, WindowManager>();
            this.container.PerRequest<IHttpService, HttpService>();          
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>
        /// The located services.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// The located service.
        /// </returns>
        protected override object GetInstance(System.Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<MainViewModel>();
        }
    }
}