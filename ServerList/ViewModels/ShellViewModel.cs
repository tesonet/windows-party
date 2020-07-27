using Caliburn.Micro;
using ServerList.Messages;

namespace ServerList.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginViewModel _loginViewModel;
        private readonly ServersViewModel _serversViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, LoginViewModel loginViewModel, ServersViewModel serversViewModel)
        {
            _eventAggregator = eventAggregator;
            _loginViewModel = loginViewModel;
            _serversViewModel = serversViewModel;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(NavigationMessage message)
        {
            switch (message.pageName)
            {
                case PageName.Login:
                    ActivateItem(_loginViewModel);
                    break;
                case PageName.ServerList:
                    ActivateItem(_serversViewModel);
                    break;
            }
        }
    }
}
