using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Models;

namespace WindowsParty.Handlers.Contracts
{
	public interface IServersHandler
	{
		Task<IEnumerable<Server>> GetServers();
	}
}
