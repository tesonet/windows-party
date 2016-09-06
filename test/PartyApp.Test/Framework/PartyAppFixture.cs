using System;
using PartyApp.Log;
using Ploeh.AutoFixture;
using Prism.Events;

namespace PartyApp.Test.Framework
{
	public class PartyAppFixture : Fixture
	{
		public PartyAppFixture()
		{
			this.Inject<IEventAggregator>(new EventAggregator());
			this.Inject<IAppLogger>(new NullLogger());
		}

		private class NullLogger : IAppLogger
		{
			public bool IsEnabled => true;

			public void Enable()
			{
			}

			public void WriteWarning(string message)
			{
			}

			public void WriteException(Exception exception)
			{
			}

			public void WriteInfo(string message)
			{
			}
		}
	}
}
