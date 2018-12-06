namespace Tesonet.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// MainViewModel class
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Conductor{Caliburn.Micro.Screen}.Collection.OneActive" />
    /// <seealso cref="Caliburn.Micro.IHandle{System.String}" />
    public class MainViewModel : Conductor<Screen>.Collection.OneActive, IHandle<string>
    {
        /// <summary>
        /// The window title default
        /// </summary>
        private const string WindowTitleDefault = "Main";

        /// <summary>
        /// The login view model
        /// </summary>
        private readonly LoginViewModel loginViewModel;

        /// <summary>
        /// The servers ListView model
        /// </summary>
        private readonly ServerListViewModel serversListViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="loginViewModel">The login view model.</param>
        /// <param name="serverListViewModel">The server ListView model.</param>
        /// <exception cref="ArgumentNullException">
        /// loginViewModel
        /// or
        /// serverListViewModel
        /// </exception>
        public MainViewModel(IEventAggregator eventAggregator, LoginViewModel loginViewModel, ServerListViewModel serverListViewModel)
        {
            this.loginViewModel = loginViewModel ?? throw new ArgumentNullException(nameof(loginViewModel));
            this.serversListViewModel = serverListViewModel ?? throw new ArgumentNullException(nameof(serverListViewModel));
            eventAggregator.Subscribe(this);
            this.ActivateItem(this.loginViewModel);
        }

        public void Handle(string message)
        {
            string msg = string.Empty;
            if (message.Substring(0, 5) == "token")
            {
                msg = message.Substring(6, message.Length - 6);
                message = "token";
            }
            else
            {
                msg = message;
            }

            switch (message)
            {
                case "Logout":
                    this.serversListViewModel.authToken = string.Empty;
                    this.ActivateItem(this.loginViewModel);
                    break;
                case "token":
                    this.serversListViewModel.authToken = msg;
                    this.ActivateItem(this.serversListViewModel);
                    break;
                default:
                    break;
            }
        }
    }
}