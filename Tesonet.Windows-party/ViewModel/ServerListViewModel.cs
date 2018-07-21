using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Tesonet.Windows_party.Models;
using Tesonet.Windows_party.Services;
using Tesonet.Windows_party.Services.Interfaces;

namespace Tesonet.Windows_party.ViewModel
{
    public class ServerListViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<ServerModel> _serverModels;

        public ServerListViewModel(IApiService apiService, INavigationService navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            ServerModels = new ObservableCollection<ServerModel>();
            InitServerModels();
        }

        public ObservableCollection<ServerModel> ServerModels
        {
            get { return _serverModels; }
            set
            {
                _serverModels = value;
                RaisePropertyChanged(() => ServerModels);
            }
        }

        public ICommand LogoutClickCommand => new RelayCommand(LogoutClick);

        private void LogoutClick()
        {
            _apiService.Logout();
            _navigationService.ShowLoginView();
        }

        private async void InitServerModels()
        {
            var serverModels = await _apiService.GetServers();
            ServerModels = new ObservableCollection<ServerModel>(serverModels);
        }
    }
}