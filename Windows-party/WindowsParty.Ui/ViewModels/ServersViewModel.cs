namespace WindowsParty.Ui.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using WindowsParty.Domain.Entities;
    using WindowsParty.Ui.Services;
    using WindowsParty.Ui.Views;

    public class ServersViewModel : ViewModelBase, IViewModel
    {
        private readonly IPageNavigationService _navigationService;

        public ServersViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
            LogOffCommand = new RelayCommand(LogOff);

            if (navigationService.Parameter is IList<Server> servers)
            {
                Servers = new BindingList<Server>(servers);
            }
            else
            {
                Servers = new BindingList<Server>();
            }
        }

        public RelayCommand LogOffCommand { get; }

        public BindingList<Server> Servers { get; }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private void LogOff()
        {
            _navigationService.NavigateTo<LogInView>();
        }
    }
}