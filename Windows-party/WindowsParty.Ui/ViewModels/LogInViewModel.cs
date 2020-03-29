namespace WindowsParty.Ui.ViewModels
{
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Domain.Models;
    using WindowsParty.Ui.Services;
    using WindowsParty.Ui.Views;

    public class LogInViewModel : ViewModelBase, IViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IServerQueryService _serverQueryService;
        private string _password;
        private string _userName;
        private string _errorMessage;

        public LogInViewModel(
            IAuthenticationService authenticationService,
            IPageNavigationService pageNavigationService,
            IServerQueryService serverQueryService)
        {
            _authenticationService = authenticationService;
            _pageNavigationService = pageNavigationService;
            _serverQueryService = serverQueryService;
            LogInCommand = new RelayCommand(async () => await LogIn(), CanExecuteLogin);
        }

        public RelayCommand LogInCommand { get; }

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                LogInCommand.RaiseCanExecuteChanged();
                ErrorMessage = string.Empty;
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                Set(ref _userName, value);
                LogInCommand.RaiseCanExecuteChanged();
                ErrorMessage = string.Empty;
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                Set(ref _errorMessage, value);
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private bool CanExecuteLogin()
        {
            return !(string.IsNullOrWhiteSpace(_userName) && string.IsNullOrWhiteSpace(_password));
        }

        private async Task LogIn()
        {
            UiServices.SetBusyState();
            var result = await _authenticationService.LogInAsync(
                new Credentials
                {
                    Password = _password,
                    Username = _userName
                });

            if (result.IsSuccess)
            {
                var servers = await _serverQueryService.GetAsync(result);
                _pageNavigationService.NavigateTo<ServersView>(servers);
            }
            else
            {
                ErrorMessage = "Wrong credentials";
            }
        }
    }
}