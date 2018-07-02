using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Unity;
using WindowsParty.Common.Interfaces;
using WindowsParty.Common.Models;
using WindowsParty.Utils;

namespace WindowsParty.ViewModels
{
    public interface IServerViewModel {}

    public class ServerViewModel : BaseViewModel, IServerViewModel
    {
        private string _errorMessage;
        private bool _isLoading;
        private readonly ILogService _logService;

        public ServerViewModel(IUnityContainer container) : base(container)
        {
            _logService = Container.Resolve<ILogService>();

            LogoutCommand = new RelayCommand(Logout);

            LoadServers();
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public async void LoadServers()
        {
            try
            {
                IsLoading = true;

                var result = await Container.Resolve<IServerService>().GetServers();

                if (!result.Success)
                {
                    _logService.Info(result.Message);
                    ErrorMessage = result.Message;
                    return;
                }

                result.Servers.ToList().ForEach(Servers.Add);
            }
            catch (Exception ex)
            {
                _logService.Error(ex);
                ErrorMessage = "System error";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public ObservableCollection<ServerResponseModel> Servers { get; } =
            new ObservableCollection<ServerResponseModel>();

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LogoutCommand { get; }

        private void Logout(object parameter)
        {
            try
            {
                Container.Resolve<IUserSessionService>().RemoveUser();
                Container.Resolve<IMainViewModel>().ShowLoginView();
            }
            catch (Exception ex)
            {
                _logService.Error(ex);
                ErrorMessage = "System error";
            }
        }
    }
}
