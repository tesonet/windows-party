using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using WindowsParty.Constants;
using WindowsParty.Handlers.Contracts;

namespace WindowsParty.ViewModels
{
	public class LoginViewModel : Screen
	{
		#region Constructors

		public LoginViewModel(ILoginHandler loginHandler,
							  IEventAggregator eventAggregator)
		{
			this.loginHandler = loginHandler;
			this.eventAggregator = eventAggregator;
		}

		#endregion

		#region Properties

		private ILoginHandler loginHandler;
		private IEventAggregator eventAggregator;

		#endregion

		#region Fields

		public string Username { get; set; }
		public string Password { get; set; }

		#endregion

		#region Methods

		public async Task LoginAsync()
		{
			if (!Validate())
				return;
			var isLoginSuccess = await loginHandler.Login(Username, Password);
			if (isLoginSuccess)
				await eventAggregator.PublishOnUIThreadAsync(typeof(ServerListViewModel));
			else
			{
				ShowMessage(DefaultValues.LOGIN_FAILED_TEXT, DefaultValues.LOGIN_FAILED_TITLE);
				Username = "";
				Password = "";
				NotifyOfPropertyChange(() => Username);
				NotifyOfPropertyChange(() => Password);
			}
		}

		private bool Validate()
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(Username))
				errors.Add(DefaultValues.ERROR_USERNAME_BLANK);

			if (string.IsNullOrWhiteSpace(Password))
				errors.Add(DefaultValues.ERROR_PASSWORD_BLANK);

			if (errors.Any())
			{
				ShowMessage(string.Join(Environment.NewLine, errors), DefaultValues.LOGIN_FAILED_TITLE);
				return false;
			}
			return true;
		}

		public virtual void ShowMessage(string message, string title)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		#endregion
	}
}
