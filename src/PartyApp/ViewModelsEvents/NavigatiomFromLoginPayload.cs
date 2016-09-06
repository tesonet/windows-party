using PartyApp.Services;
using PartyApp.Utilities;

namespace PartyApp.ViewModelsEvents
{
	public class NavigatiomFromLoginPayload : Payload
	{
		public NavigatiomFromLoginPayload(AuthorizationToken authorizationToken)
		{
			Guard.NotNull(authorizationToken, nameof(authorizationToken));
			AuthorizationToken = authorizationToken;
		}

		public AuthorizationToken AuthorizationToken { get; }
	}
}
