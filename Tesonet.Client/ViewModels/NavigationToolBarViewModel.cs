using GalaSoft.MvvmLight.Command;
using Tesonet.Client.Messages;
using Tesonet.Client.Services.MessengerService;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.ViewModels
{
    public class NavigationToolBarViewModel : NavigableViewModel
    {
        private readonly IMessengerService _messenger;

        private RelayCommand _gotoServersPageCommand;
        private RelayCommand _gotoSettingsPageCommand;
        private RelayCommand _gotoLoginPageCommand;

        public NavigationToolBarViewModel(INavigationService navigationService, IMessengerService messenger) : base(navigationService)
        {
            _messenger = messenger;

            _messenger.Default.Register<IsBusyChangedMessage>(this, (message) =>
            {
                if (!(message.Source is NavigationToolBarViewModel))
                {
                    return;
                }

                GotoServersPageCommand.RaiseCanExecuteChanged();
                GotoSettingsPageCommand.RaiseCanExecuteChanged();
                GotoLoginPageCommand.RaiseCanExecuteChanged();
            });
        }

        public RelayCommand GotoServersPageCommand => _gotoServersPageCommand ?? (_gotoServersPageCommand = new RelayCommand(GotoServersPage, () => !IsBusy));
        public RelayCommand GotoSettingsPageCommand => _gotoSettingsPageCommand ?? (_gotoSettingsPageCommand = new RelayCommand(GotoSettingsPage, () => !IsBusy));
        public RelayCommand GotoLoginPageCommand => _gotoLoginPageCommand ?? (_gotoLoginPageCommand = new RelayCommand(GotoLoginPage, () => !IsBusy));

        private async void GotoServersPage()
        {
            await ExecuteBusyActionAsync(() => NavigationService.NavigateToServersPageAsync(null), string.Empty);
        }

        private async void GotoSettingsPage()
        {
            await ExecuteBusyActionAsync(() => NavigationService.NavigateToSettingsPageAsync(null), string.Empty);
        }

        private async void GotoLoginPage()
        {
            await ExecuteBusyActionAsync(() => NavigationService.NavigateToLoginPageAsync(null), string.Empty);
        }
    }
}