// <copyright file="AppBootstrapper.cs" company="Steve Martin">
// Copyright (c) Steve Martin, 2012. All Right Reserved.
// </copyright>
// <license Name="Modified MIT License"
// Source="http://kihonkai.com/content/software-license-sample-code">
// Full text in LICENSE.TXT file.
// </license>
namespace CaliburnMicro.LoginTestExternal.Framework
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;

    using Caliburn.Micro;

    using CaliburnMicro.LoginTestExternal.Model;
    using CaliburnMicro.LoginTestExternal.ViewModels;

    /// <summary>
    /// Entry point into the application.
    /// </summary>
    public class AppBootstrapper : Bootstrapper
    {
        #region Fields

        /// <summary>
        /// Reference to the Log class
        /// </summary>
        private static readonly ILog Log;

        /// <summary>
        /// Flag if the log file should be deleted each time the app runs
        /// </summary>
        private static bool logFileDeleted;

        /// <summary>
        /// MEF container
        /// </summary>
        private CompositionContainer container;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="AppBootstrapper"/> class
        /// and defines the logger class to use.
        /// </summary>
        static AppBootstrapper()
        {
            ////LogManager.GetLog = type => new DebugLogger(type);
            logFileDeleted = false;
            LogManager.GetLog = type => new SimpleFileLogger(type, () => CanDeleteLogFile());
            Log = LogManager.GetLog(typeof(AppBootstrapper));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            this.container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x =>
                                                                            new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            CompositionBatch batch = new CompositionBatch();

            batch.AddExportedValue<ILoginService>(new MockLoginService());
            batch.AddExportedValue<IWindowManager>(new AppWindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this.container);

            this.container.Compose(batch);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns>The required instance based on the parameters</returns>
        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = this.container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        /// <summary>
        /// Called when [startup].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            Log.Info("App Startup!");
            ILoginConductor loginConductor;
            IEventAggregator events;
            loginConductor = this.container.GetExportedValue<ILoginConductor>();
            events = this.container.GetExportedValue<IEventAggregator>();
            events.Publish(new LoginEvent());
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Log.Info("App Exit!");
            base.OnExit(sender, e);
        }

        /// <summary>
        /// Determines whether this instance [can delete log file].
        /// </summary>
        /// <remarks>
        /// Random way I could think to only delete the log file once since 
        /// LogManager.GetLog creates a new instance of the ILog class
        /// in each class.
        /// </remarks>
        /// <returns>
        ///   <c>true</c> if this instance [can delete log file]; otherwise, <c>false</c>.
        /// </returns>
        private static bool CanDeleteLogFile()
        {
            bool output;
            if (!logFileDeleted)
            {
                output = true;
                logFileDeleted = true;
            }
            else
            {
                output = false;
            }

            return output;
        }

        #endregion Methods
    }
}