using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PartyApp.Models;
using PartyApp.Properties;
using PartyApp.Services;
using PartyApp.Utilities;
using PartyApp.ViewModelsEvents;
using Prism.Commands;
using Prism.Events;

namespace PartyApp.ViewModels
{
	public class LoginViewModel : PartyAppViewModel
	{
		private readonly ILoginModel _loginModel;
		private readonly DelegateCommand _loginCommand;
		private string _userName;
		private string _password;
		private string _errorMessage;
		private bool _loginInProgress;

		public LoginViewModel(
			ILoginModel loginModel,
			IEventAggregator eventAggregator)
			: base(eventAggregator)
		{
			Guard.NotNull(loginModel, nameof(loginModel));
			_loginModel = loginModel;

			_loginCommand = DelegateCommand.FromAsyncHandler(
				() => OnLoginAsync(),
				() => !LoginInProgress).
				ObservesProperty(() => LoginInProgress);
		}

		public string UserName
		{
			get { return _userName; }
			set { SetProperty(ref _userName, value); }
		}

		public string Password
		{
			get { return _password; }
			set { SetProperty(ref _password, value); }
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { SetProperty(ref _errorMessage, value); }
		}

		public ICommand LoginCommand
		{
			get { return _loginCommand; }
		}

		private bool LoginInProgress
		{
			get { return _loginInProgress; }
			set { SetProperty(ref _loginInProgress, value); }
		}

		private async Task OnLoginAsync()
		{
			ClearError();

			if (ValidateCredentials())
			{
				try
				{
					//viewmodel decides how much it should wait for authorization
					var cancellationTokenSource = new CancellationTokenSource(20000);
					LoginInProgress = true;

					AuthorizationResult result = await _loginModel.Login(
						_userName,
						_password,
						cancellationTokenSource.Token);

					if (result.Success)
					{
						EventAggregator.GetEvent<NavigationFromLoginRequestedEvent>().Publish(
							new NavigatiomFromLoginPayload(result.Token));
					}
					else
					{
						NotifyErrorOccured(result.ErrorMessage);
					}
				}
				catch (OperationCanceledException)
				{
					NotifyErrorOccured(Resources.LoginTimedOut);
				}
				catch (Exception ex)
				{
					NotifyErrorOccured(ex);
				}
				finally
				{
					LoginInProgress = false;
				}
			}
		}

		private bool ValidateCredentials()
		{
			if (string.IsNullOrWhiteSpace(_userName))
			{
				ErrorMessage = Resources.UserNameIsRequired;
				return false;
			}

			if (string.IsNullOrWhiteSpace(_password))
			{
				ErrorMessage = Resources.PasswordIsRequired;
				return false;
			}

			return true;
		}

		private void ClearError()
		{
			ErrorMessage = "";
		}
	}
}
