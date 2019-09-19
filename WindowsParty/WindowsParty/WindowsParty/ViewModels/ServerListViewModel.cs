using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Models;

namespace WindowsParty.ViewModels
{
	public class ServerListViewModel : Screen
	{
		#region Fields

		public ObservableCollection<Server> Servers { get; set; }
		private IServersHandler serversHandler;

		#endregion

		#region Constructors

		public ServerListViewModel(IServersHandler serversHandler)
		{
			this.serversHandler = serversHandler;
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

		#endregion
	}
}
