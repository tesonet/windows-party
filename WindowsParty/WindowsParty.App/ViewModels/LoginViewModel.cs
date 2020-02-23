using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows.Controls;
using WindowsParty.App.Domain;
using WindowsParty.App.Domain.Commands;
using WindowsParty.App.Domain.Events;
using WindowsParty.App.Services.Models;

namespace WindowsParty.App.ViewModels
{
	public class LoginViewModel : Conductor<object>.Collection.OneActive,
		IHandle<TokenSetEvent>,
		IHandleWithTask<LoginFailedEvent>
	{
		private string _userName;
		private string _password;
		private bool _isUiEnabled = true;
		private bool _canLogin;

		private readonly IEventAggregator _eventAggregator;
		private readonly ICommandProcessor _commandProcessor;
		private readonly ITokenService _tokenService;

		public LoginViewModel(IEventAggregator eventAggregator, ICommandProcessor commandProcessor, ITokenService tokenService)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.Subscribe(this);

			_tokenService = tokenService;

			_commandProcessor = commandProcessor;
		}

		public string UserName
		{
			get { return _userName; }
			set 
			{ 
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
				CanLogin = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(Password);
			}
		}

		public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value;
				NotifyOfPropertyChange(() => Password);
				CanLogin = !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(value);
			}
		}

		public bool IsUiEnabled
		{
			get { return _isUiEnabled; }
			set
			{
				_isUiEnabled = value;
				NotifyOfPropertyChange(() => IsUiEnabled);
			}
		}

		public bool CanLogin
		{
			get { return _canLogin; }
			set 
			{ 
				_canLogin = value;
				NotifyOfPropertyChange(() => CanLogin);
			}
		}


		public void OnPasswordChanged(PasswordBox source)
		{
			Password = source.Password;
		}

		public async Task Login(string userName, string password)
		{
			IsUiEnabled = false;

			await _commandProcessor.ProcessAsync(new LoginUserCommand(_userName, _password)).ConfigureAwait(false);
		}

		public void Handle(TokenSetEvent message)
		{
			_tokenService.Token = message.Token;

			var conductor = Parent as IConductor;
			conductor.ActivateItem(IoC.Get<ServerViewModel>());
		}

		public async Task Handle(LoginFailedEvent message)
		{
			IsUiEnabled = true;

			await DialogHost.Show(new { ErrorMessage = message.Message });
		}
	}
}
