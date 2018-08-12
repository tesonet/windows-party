using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.ViewModels
{
    public class MainPageViewModel : NavigableViewModel
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();

        private NavigationToolBarViewModel _navigationToolBar;
        private ViewModelBase _selectedPage;

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        public NavigationToolBarViewModel NavigationToolBar
        {
            get => _navigationToolBar;
            set
            {
                _navigationToolBar = value;
                RaisePropertyChanged();
            }
        }

        public ViewModelBase SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                RaisePropertyChanged();
            }
        }

        public override async Task InitializeAsync(NavigationData navigationData)
        {
            var toolBar = _locator.NavigationToolBarViewModel;
            NavigationToolBar = toolBar;
            await toolBar.InitializeAsync(null);
        }
    }
}