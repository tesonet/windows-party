using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Models;

namespace WindowsParty.Handlers
{
	public class ServersHandler : HandlerBase, IServersHandler
	{
		#region Properties

		protected override string Endpoint => "servers";

		#endregion

		#region Methods

		public async Task<IEnumerable<Server>> GetServers()
		{
			return await Get<Server>();
		}

		#endregion
	}
}
