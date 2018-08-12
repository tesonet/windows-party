using GalaSoft.MvvmLight.Command;
using Tesonet.Client.Helpers;
using Tesonet.Client.Properties;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.ViewModels
{
    public class SettingsPageViewModel : NavigableViewModel
    {
        private readonly ISettings _settings;

        private string _serverAuthUrl;
        private string _serverServersUrl;
        private bool _needSave;

        private RelayCommand _saveCommand;

        public SettingsPageViewModel(INavigationService navigationService, ISettings settings) : base(navigationService)
        {
            _settings = settings;
            _serverAuthUrl = _settings.ServerAuthUrl;
            _serverServersUrl = _settings.ServerServersUrl;
        }

        public string ServerAuthUrl
        {
            get => _serverAuthUrl;
            set
            {
                _serverAuthUrl = value;
                RaisePropertyChanged();

                _needSave = true;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string ServerServersUrl
        {
            get => _serverServersUrl;
            set
            {
                _serverServersUrl = value;
                RaisePropertyChanged();

                _needSave = true;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save, () => _needSave));

        private void Save()
        {
            Log.Info(Resources.SavingSettings);

            _settings.ServerAuthUrl = ServerAuthUrl;
            _settings.ServerServersUrl = ServerServersUrl;

            _needSave = false;
            SaveCommand.RaiseCanExecuteChanged();
        }
    }
}