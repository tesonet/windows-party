using System;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WindowsParty.Models;
using WindowsParty.Services;
using EnsureThat;
using System.Linq;

namespace WindowsParty.ViewModels
{
	public class ServersViewModel : Screen
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IServersService _serversService;
		private string _userError;

		public ServersViewModel(IEventAggregator eventAggregator, IServersService serversService)
		{
			EnsureArg.IsNotNull(eventAggregator, nameof(eventAggregator));
			EnsureArg.IsNotNull(serversService, nameof(serversService));

			_eventAggregator = eventAggregator;
			_serversService = serversService;
		}

		public BindableCollection<Server> Servers { get; } = new BindableCollection<Server>();

		public string UserError
		{
			get => _userError;
			set
			{
				_userError = value;
				NotifyOfPropertyChange();
			}
		}

		public bool HasErrors => !string.IsNullOrEmpty(UserError);

		protected override async void OnActivate()
		{
			try
			{
				Servers.AddRange(await _serversService.GetServersAsync());
			}
			catch (Exception ex)
			{
				UserError = $"Failed to get servers info from service: {ex.Message}";
			}
		}

		public void Logout()
		{
			_eventAggregator.PublishOnUIThread(EventMessages.LogoutPressed);
			Servers.Clear();
		}
	}
}