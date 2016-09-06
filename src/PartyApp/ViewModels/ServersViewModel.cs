using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
	public class ServersViewModel : PartyAppViewModel
	{
		private readonly IServersModel _serversModel;

		public ServersViewModel(
			IServersModel serversModel,
			IEventAggregator eventAggregator)
			: base(eventAggregator)
		{
			Guard.NotNull(serversModel, nameof(serversModel));
			_serversModel = serversModel;

			LogoutCommand = new DelegateCommand(Logout);
			EventAggregator.GetEvent<NavigationFromLoginRequestedEvent>().Subscribe(InnerGetServersAsync);
		}

		public ObservableCollection<Server> Servers { get; } = new ObservableCollection<Server>();

		public ICommand LogoutCommand { get; }

		public async Task GetServersAsync(AuthorizationToken token)
		{
			Guard.NotNull(token, nameof(token));

			//viewmodel decides how much it should wait for servers
			var cancellationTokenSource = new CancellationTokenSource(20000);

			IEnumerable<Server> result = null;
			try
			{
				result = await _serversModel.GetServersAsync(
					token, cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
				NotifyErrorOccured(Resources.ServersRetrievalTimedOut);
			}
			catch (Exception ex)
			{
				NotifyErrorOccured(ex);
			}

			Servers.Clear();
			if (result != null)
				Servers.AddRange(result.OrderBy(s => s.Name));
		}

		public void Logout()
		{
			EventAggregator.GetEvent<NavigationToLoginRequestedEvent>().Publish(Payload.Empty);
		}

		private async void InnerGetServersAsync(NavigatiomFromLoginPayload payload)
		{
			//void returning async methods should not be public
			await GetServersAsync(payload.AuthorizationToken);
		}
	}
}
