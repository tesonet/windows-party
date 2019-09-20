using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WindowsParty.Constants;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Models;

namespace WindowsParty.ViewModels
{
	public class ServerListViewModel : Screen
	{
		#region Fields

		public ObservableCollection<Server> Servers { get; set; }
		private IServersHandler serversHandler;
		private IEventAggregator eventAggregator;

		#endregion

		#region Constructors

		public ServerListViewModel(IServersHandler serversHandler,
								   IEventAggregator eventAggregator)
		{
			this.serversHandler = serversHandler;
			this.eventAggregator = eventAggregator;
			Servers = new ObservableCollection<Server>();			
		}

		#endregion

		#region Methods

		protected override void OnInitialize()
		{
			base.OnInitialize();
			UpdateServers();
		}

		private async Task UpdateServers()
		{
			var serversList = await serversHandler.GetServers();
			Servers.Clear();
			foreach (var server in serversList)
				Servers.Add(server);
		}
		
		public async Task LogoutAsync()
		{
			App.Current.Properties.Remove(DefaultValues.TOKEN);
			await eventAggregator.PublishOnUIThreadAsync(typeof(LoginViewModel));
		}

		#endregion
	}
}
