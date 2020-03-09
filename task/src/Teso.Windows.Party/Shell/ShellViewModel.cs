using Caliburn.Micro;
using Teso.Windows.Party.Events;
using Teso.Windows.Party.Login;
using Teso.Windows.Party.Models;
using Teso.Windows.Party.ServerList;

namespace Teso.Windows.Party.Shell
{
    class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<ChangeEvent>
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly ServerListViewModel _serverListViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, LoginViewModel loginViewModel, ServerListViewModel serverListViewModel)
        {
            eventAggregator.Subscribe(this);

            _loginViewModel = loginViewModel;
            _serverListViewModel = serverListViewModel;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ActivateItem(_loginViewModel);
        }

        public void Handle(ChangeEvent message)
        {
            switch (message.ChangeAction)
            {
                case ChangeAction.LoggedIn:
                    IoC.Get<User>().Token = (string)message.Data;
                    ActivateItem(_serverListViewModel);
                    break;

                case ChangeAction.LoggedOut:
                    IoC.Get<User>().Token = null;
                    ActivateItem(_loginViewModel);
                    break;
            }
        }
    }
}
