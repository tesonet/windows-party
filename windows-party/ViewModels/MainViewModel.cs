using Autofac;
using Tesonet.Windows.Party.Models;

namespace Tesonet.Windows.Party.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private LogInViewModel _loginViewModel;
        private ServerListViewModel _serverListViewModel;

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public MainViewModel()
        {
            _loginViewModel = Bootstrapper.Container.Resolve<LogInViewModel>();
            _serverListViewModel = Bootstrapper.Container.Resolve<ServerListViewModel>();
            CurrentViewModel = _loginViewModel;
            _loginViewModel.LogInSuccessful += OnLogIn;
            _serverListViewModel.LogOut += OnLogOut;
        }

        private async void OnLogIn(User user)
        {
            _serverListViewModel.LoggedInUser = user;
            CurrentViewModel = _serverListViewModel;
            await _serverListViewModel.LoadServers();
        }

        private void OnLogOut()
        {
            _loginViewModel.Password = string.Empty;
            CurrentViewModel = _loginViewModel;
        }
    }
}
