using Prism.Events;

namespace PartyApp.ViewModelsEvents
{
	public sealed class NavigationFromLoginRequestedEvent : PubSubEvent<NavigatiomFromLoginPayload>
	{
	}
}
