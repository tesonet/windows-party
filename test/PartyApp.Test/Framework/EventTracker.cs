using Prism.Events;

namespace PartyApp.Test.Framework
{
	public class EventTracker<T, TPayload> where T : PubSubEvent<TPayload>, new()
	{
		private readonly IEventAggregator m_Aggregator;

		public EventTracker(IEventAggregator aggregator)
		{
			m_Aggregator = aggregator;
			aggregator.GetEvent<T>().Subscribe(payload =>
			{
				IsPublished = true;
			});
		}

		public bool IsPublished { get; private set; }
	}
}
