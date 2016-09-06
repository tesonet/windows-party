using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PartyApp.Services;
using PartyApp.Utilities;

namespace PartyApp.Models
{
	public class ServersModel : IServersModel
	{
		private IServersProvider _serversProvider;

		public ServersModel(IServersProvider serversProvider)
		{
			Guard.NotNull(serversProvider, nameof(serversProvider));
			_serversProvider = serversProvider;
		}

		public async Task<IEnumerable<Server>> GetServersAsync(
			AuthorizationToken authorizationToken,
			CancellationToken cancellationToken)
		{
			Guard.NotNull(authorizationToken, nameof(authorizationToken));
			return await _serversProvider.GetServersAsync(
				authorizationToken, cancellationToken);
		}
	}
}