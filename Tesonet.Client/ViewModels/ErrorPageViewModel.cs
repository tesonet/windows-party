using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.ViewModels
{
    public class ErrorPageViewModel : NavigableViewModel
    {
        private string _errorTitle;
        private string _errorMessage;

        private RelayCommand _okCommand;

        public ErrorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public NavigableViewModel NavigatedFromPage { get; set; }

        public string ErrorTitle
        {
            get => _errorTitle;
            set { _errorTitle = value; RaisePropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; RaisePropertyChanged(); }
        }

        public RelayCommand OkCommand => _okCommand ?? (_okCommand = new RelayCommand(Ok));

        public override async Task InitializeAsync(NavigationData navigationData)
        {
            var errorPageNavigationData = navigationData as ErrorPageNavigationData;
            if (errorPageNavigationData != null)
            {
                ErrorTitle = errorPageNavigationData.ErrorTitle;
                ErrorMessage = errorPageNavigationData.ErrorMessage;
                NavigatedFromPage = errorPageNavigationData.NavigatedFromPage;
            }

            await base.InitializeAsync(navigationData);
        }

        private async void Ok()
        {
            await NavigationService.NavigateToPageAsync(NavigatedFromPage, null);
        }
    }
}