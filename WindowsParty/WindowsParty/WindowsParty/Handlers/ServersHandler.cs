using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Models;

namespace WindowsParty.Handlers
{
	public class ServersHandler : HandlerBase, IServersHandler
	{
		#region Properties

		protected override string Endpoint => "servers";

		#endregion

		#region Constructors

		public ServersHandler(SimpleContainer container) : base(container)
		{
		}

		#endregion

		#region Methods

		public async Task<IEnumerable<Server>> GetServers()
		{
			return await Get<Server>();
		}

		#endregion
	}
}
