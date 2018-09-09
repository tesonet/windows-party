using WindowsParty.Core.Responses;
using Caliburn.Micro;

namespace WindowsParty.UI.Windows.ViewModels
{
    // https://caliburnmicro.com/documentation/4.0.0/event-aggregator
    public class ShellViewModel : Conductor<object>, IHandle<TokenResponse>, IHandle<LogoutResponse>
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly ServerListViewModel _serverListViewModel;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(LoginViewModel loginViewModel, ServerListViewModel serverListViewModel, IEventAggregator eventAggregator)
        {
            _loginViewModel = loginViewModel;
            _serverListViewModel = serverListViewModel;
            _eventAggregator = eventAggregator;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginViewModel);
        }

        public void Handle(TokenResponse message)
        {
            _serverListViewModel.Token = message.Token; // not the best way
            ActivateItem(_serverListViewModel);
        }

        public void Handle(LogoutResponse message)
        {
            ActivateItem(_loginViewModel);
        }
    }
}