using PartyApp.Properties;
using PartyApp.Utilities;
using PartyApp.ViewModelsEvents;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace PartyApp.ViewModels
{
	public class MainWindowViewModel : PartyAppViewModel
	{
		private bool _loginViewVisible = true;
		private bool _serversViewVisible;

		public MainWindowViewModel(
			IEventAggregator eventAggregator,
			InteractionRequest<INotification> errorNotification)
			: base(eventAggregator)
		{
			Guard.NotNull(errorNotification, nameof(errorNotification));
			ErrorNotification = errorNotification;

			EventAggregator.GetEvent<NavigationToLoginRequestedEvent>().Subscribe(InnerNavigateToLogin);
			EventAggregator.GetEvent<NavigationFromLoginRequestedEvent>().Subscribe(InnerNavigateFromLogin);
			EventAggregator.GetEvent<ErrorEvent>().Subscribe(HandleError);
		}

		public InteractionRequest<INotification> ErrorNotification { get; }

		public bool LoginViewVisible
		{
			get { return _loginViewVisible; }
			set { SetProperty(ref _loginViewVisible, value); }
		}

		public bool ServersViewVisible
		{
			get { return _serversViewVisible; }
			set { SetProperty(ref _serversViewVisible, value); }
		}

		public void NavigateFromLogin()
		{
			ServersViewVisible = true;
			LoginViewVisible = false;
		}

		public void NavigateToLogin()
		{
			LoginViewVisible = true;
			ServersViewVisible = false;
		}

		private void HandleError(ErrorPayload payload)
		{
			var notification = new Notification()
			{
				Title = Resources.ApplicationName
			};

			if (payload.FromException)
			{
				notification.Content = Resources.UnexpectedError;
				ErrorNotification.Raise(notification);
				return;
			}

			string errorMessage = payload.GetErrorMessage();
			notification.Content = errorMessage;
			ErrorNotification.Raise(notification);
		}

		private void InnerNavigateFromLogin(NavigatiomFromLoginPayload payload)
		{
			//keep public API clean, hide the implementation details
			NavigateFromLogin();

		}

		private void InnerNavigateToLogin(Payload payload)
		{
			//keep public API clean, hide the implementation details
			NavigateToLogin();
		}
	}
}
