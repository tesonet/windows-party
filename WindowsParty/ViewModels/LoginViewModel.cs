using System;
using Caliburn.Micro;
using WindowsParty.Services;
using EnsureThat;

namespace WindowsParty.ViewModels
{
	public class LoginViewModel : Screen
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IServersService _serversService;
		private string _userError;
		private string _username;
		private string _password;

		public LoginViewModel(IEventAggregator eventAggregator, IServersService serversService)
		{
			EnsureArg.IsNotNull(eventAggregator, nameof(eventAggregator));
			EnsureArg.IsNotNull(serversService, nameof(serversService));

			_eventAggregator = eventAggregator;
			_serversService = serversService;
		}

		public string Username
		{
			get => _username;
			set
			{
				_username = value;
				NotifyOfPropertyChange();
			}
		}

		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				NotifyOfPropertyChange();
			}
		}

		public string UserError
		{
			get => _userError;
			set
			{
				_userError = value;
				NotifyOfPropertyChange();
			}
		}

		public async void Login()
		{
			if (!ValidateInputs(out string reason))
			{
				UserError = reason;
				ClearInputs();
				return;
			}

			try
			{
				var authorized = await _serversService.AuthorizeAsync(Username, Password);
				if (authorized)
				{
					UserError = string.Empty;
					_eventAggregator.PublishOnUIThread(EventMessages.LoginPressed);
				}
				else
				{
					UserError = "Unknown Username or bad Password entered.";
				}
			}
			catch (Exception ex)
			{
				UserError = $"An unknown authentication error has occurred. Exception: {ex.Message}.";
			}

			ClearInputs();
		}

		private bool ValidateInputs(out string reason)
		{
			if (string.IsNullOrEmpty(Username))
			{
				reason = "Username cannot be empty.";
				return false;
			}

			if (string.IsNullOrEmpty(Password))
			{
				reason = "Password cannot be empty.";
				return false;
			}

			reason = string.Empty;
			return true;
		}

		private void ClearInputs()
		{
			Username = string.Empty;
			Password = string.Empty;
		}
	}
}