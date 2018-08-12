using Caliburn.Micro;
using System;
using EnsureThat;

namespace WindowsParty.ViewModels
{
	public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<EventMessages>
	{
		private readonly LoginViewModel _loginViewModel;
		private readonly ServersViewModel _serversViewModel;

		public ShellViewModel(LoginViewModel loginViewModel, ServersViewModel servers, IEventAggregator eventAggregator)
		{
			EnsureArg.IsNotNull(loginViewModel, nameof(loginViewModel));
			EnsureArg.IsNotNull(servers, nameof(servers));
			EnsureArg.IsNotNull(eventAggregator, nameof(eventAggregator));

			_loginViewModel = loginViewModel;
			_serversViewModel = servers;
			eventAggregator.Subscribe(this);
			Items.AddRange(new Screen[] {_loginViewModel, _serversViewModel });
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
			ActivateItem(_loginViewModel);
		}

		public void Handle(EventMessages message)
		{
			switch (message)
			{
				case EventMessages.LoginPressed:
					ActivateItem(_serversViewModel);
					break;
				case EventMessages.LogoutPressed:
					ActivateItem(_loginViewModel);
					break;
				default:
					throw new InvalidOperationException($"Unknown event message: {message}.");
			}
		}
	}
}
