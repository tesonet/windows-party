using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PartyApp.Services;

namespace PartyApp.Models
{
	public interface IServersModel
	{
		Task<IEnumerable<Server>> GetServersAsync(
			AuthorizationToken authorizationToken,
			CancellationToken cancellationToken);
	}
}