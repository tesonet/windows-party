using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PartyApp.Services
{
	public interface IServersProvider
	{
		Task<IEnumerable<Server>> GetServersAsync(
			AuthorizationToken authorizationToken,
			CancellationToken cancellationToken);
	}
}