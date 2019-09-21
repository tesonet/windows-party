using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsParty.Constants;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Helpers;
using WindowsParty.Views;

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
			try
			{
				if (!Validate())
					return;
				var isLoginSuccess = await loginHandler.Login(Username, Password);
				if (isLoginSuccess)
					await NavigateToServerListScreen();
				else
				{
					ShowMessage(DefaultValues.LOGIN_FAILED_TEXT, DefaultValues.LOGIN_FAILED_TITLE);
					ClearCredentials();
				}
			}
			catch (Exception ex)
			{
				MessageViewer.ShowError(ex);
			}
		}

		private void ClearCredentials()
		{
			Username = "";
			Password = "";

			NotifyOfPropertyChange(() => Username);

			var view = (this.GetView() as LoginView);
			view?.ClearPassword();
		}

		/// <summary>
		/// We can't mock method `IEventAggregator.PublishOnUiThreadAsync`, so we need this one for 
		/// overriding during testing.
		/// </summary>
		/// <returns></returns>
		public virtual async Task NavigateToServerListScreen()
		{
			await eventAggregator.PublishOnUIThreadAsync(typeof(ServerListViewModel));
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
			MessageViewer.ShowError(message, title);
		}

		#endregion
	}
}
