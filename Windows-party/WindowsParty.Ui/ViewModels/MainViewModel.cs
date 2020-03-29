namespace WindowsParty.Ui.ViewModels
{
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using WindowsParty.Ui.Services;
    using WindowsParty.Ui.Views;

    public class MainViewModel : ViewModelBase, IViewModel
    {
        private RelayCommand _loadedCommand;
        private IPageNavigationService _navigationService;

        public MainViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand
                    ?? (_loadedCommand = new RelayCommand(
                        () =>
                        {
                            _navigationService.NavigateTo<LogInView>();
                        }));
            }
        }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}