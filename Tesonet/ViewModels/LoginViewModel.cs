namespace Tesonet.ViewModels
{
    using System;
    using Caliburn.Micro;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tesonet.Models;
    using Tesonet.Services;

    /// <summary>
    /// LoginViewModel class
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class LoginViewModel : Screen
    {
        /// <summary>
        /// The error message
        /// </summary>
        private string errorMessage = string.Empty;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The HTTP service
        /// </summary>
        private readonly IHttpService httpService;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set
            {
                this.errorMessage = value;
                NotifyOfPropertyChange(() => this.errorMessage);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="httpService">The HTTP service.</param>
        public LoginViewModel(IEventAggregator eventAggregator, IHttpService httpService)
        {
            this.eventAggregator = eventAggregator;
            this.httpService = httpService;
        }

        /// <summary>
        /// Logins the specified text username.
        /// </summary>
        /// <param name="txtUsername">The text username.</param>
        /// <param name="txtPassword">The text password.</param>
        /// <returns>Empty task</returns>
        public async Task Login(string txtUsername, string txtPassword)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("username", txtUsername);
            headers.Add("password", txtPassword);
            
            var LogIn = await this.httpService.PostDataAsync<LoginResponse>("http://playground.tesonet.lt/v1/tokens", headers);
            
            if (LogIn.token == null)
            {
                this.errorMessage = "Wrong Login";
            }
            else
            {
                this.errorMessage = string.Empty;
                this.eventAggregator.PublishOnUIThread("token=" + LogIn.token);
            }
        }

        /// <summary>
        /// Notifies the of property change.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void NotifyOfPropertyChange(Func<string> p)
        {
            throw new NotImplementedException();
        }
    }
}