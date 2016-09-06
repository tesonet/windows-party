using System;
using PartyApp.Utilities;
using PartyApp.ViewModelsEvents;
using Prism.Events;
using Prism.Mvvm;

namespace PartyApp.ViewModels
{
	public abstract class PartyAppViewModel : BindableBase
	{
		public PartyAppViewModel(IEventAggregator eventAggregator)
		{
			Guard.NotNull(eventAggregator, nameof(eventAggregator));
			EventAggregator = eventAggregator;
		}

		protected IEventAggregator EventAggregator { get; }

		protected void NotifyErrorOccured(string message)
		{
			EventAggregator.GetEvent<ErrorEvent>().Publish(new ErrorPayload(message));
		}

		protected void NotifyErrorOccured(Exception exception)
		{
			EventAggregator.GetEvent<ErrorEvent>().Publish(new ErrorPayload(exception));
		}
	}
}
