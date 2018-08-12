using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.Client.ViewModels;
using Tesonet.Client.ViewModels.Base;
using NavigationService = Tesonet.Client.Services.NavigationService.NavigationService;

namespace Tesonet.Client.Tests.ServicesTests
{
    [TestClass]
    public class NavigationServiceTests : BaseTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _viewModelProviderMock.Setup(x => x.MainViewModel).Returns(new MainViewModel(_navigationServiceMock.Object,
                _authorizationServiceMock.Object,
                _settingsServiceMock.Object));
            _viewModelProviderMock.Setup(x => x.MainPageViewModel).Returns(new MainPageViewModel(_navigationServiceMock.Object));
            _viewModelProviderMock.Setup(x => x.LoginViewModel).Returns(new LoginViewModel(_navigationServiceMock.Object,
                _authorizationServiceMock.Object,
                _settingsServiceMock.Object));
            _viewModelProviderMock.Setup(x => x.ServersPageViewModel).Returns(new ServersPageViewModel(_navigationServiceMock.Object,
                _serversServiceMock.Object,
                _settingsServiceMock.Object));
            _viewModelProviderMock.Setup(x => x.SettingsPageViewModel).Returns(new SettingsPageViewModel(_navigationServiceMock.Object,
                _settingsServiceMock.Object));
            _viewModelProviderMock.Setup(x => x.NavigationToolBarViewModel).Returns(new NavigationToolBarViewModel(_navigationServiceMock.Object, _messengerServiceMock.Object));
            _viewModelProviderMock.Setup(x => x.ErrorPageViewModel).Returns(new ErrorPageViewModel(_navigationServiceMock.Object));
        }

        [TestMethod]
        public async Task ShouldSetLoginViewModelAsMainPage_WhenNavigateToLoginPageAsync()
        {
            //arrange
            var viewModelProvider = _viewModelProviderMock.Object;
            var navigationService = new NavigationService(viewModelProvider);

            //act
            await navigationService.NavigateToLoginPageAsync(null);
            
            //assert
            Assert.IsInstanceOfType(viewModelProvider.MainViewModel.MainPage, typeof(LoginViewModel));
        }

        [TestMethod]
        public async Task ShouldSetMainPageViewModelAsMainPage_WhenNavigateToMainPageAsync()
        {
            //arrange
            var viewModelProvider = _viewModelProviderMock.Object;
            var navigationService = new NavigationService(viewModelProvider);

            //act
            await navigationService.NavigateToMainPageAsync(null);

            //assert
            Assert.IsInstanceOfType(viewModelProvider.MainViewModel.MainPage, typeof(MainPageViewModel));
        }

        [TestMethod]
        public async Task ShouldSetMainPageViewModelAsMainPageAndSelectedPageToServersPageViewModel_WhenNavigateToServersPageAsync()
        {
            //arrange
            var viewModelProvider = _viewModelProviderMock.Object;
            var navigationService = new NavigationService(viewModelProvider);

            //act
            await navigationService.NavigateToServersPageAsync(null);

            //assert
            Assert.IsInstanceOfType(viewModelProvider.MainViewModel.MainPage, typeof(MainPageViewModel));
            Assert.IsInstanceOfType(viewModelProvider.MainPageViewModel.SelectedPage, typeof(ServersPageViewModel));
        }

        [TestMethod]
        public async Task ShouldSetMainPageViewModelAsMainPageAndSelectedPageToSettingsPageViewModel_WhenNavigateToSettingsPageAsync()
        {
            //arrange
            var viewModelProvider = _viewModelProviderMock.Object;
            var navigationService = new NavigationService(viewModelProvider);

            //act
            await navigationService.NavigateToSettingsPageAsync(null);

            //assert
            Assert.IsInstanceOfType(viewModelProvider.MainViewModel.MainPage, typeof(MainPageViewModel));
            Assert.IsInstanceOfType(viewModelProvider.MainPageViewModel.SelectedPage, typeof(SettingsPageViewModel));
        }

        [TestMethod]
        public async Task ShouldSetMainPageAsErrorPageViewModel_WhenNavigateToErrorPageAsync()
        {
            //arrange
            var viewModelProvider = _viewModelProviderMock.Object;
            var navigationService = new NavigationService(viewModelProvider);

            //act
            await navigationService.NavigateToErrorPageAsync(null);

            //assert
            Assert.IsInstanceOfType(viewModelProvider.MainViewModel.MainPage, typeof(ErrorPageViewModel));
        }
    }
}