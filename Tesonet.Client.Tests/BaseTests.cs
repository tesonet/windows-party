using GalaSoft.MvvmLight.Messaging;
using Moq;
using Tesonet.Client.Helpers;
using Tesonet.Client.Services;
using Tesonet.Client.Services.MessengerService;
using Tesonet.Client.Services.NavigationService;
using Tesonet.ServerProxy.Services.AuthorizationService;
using Tesonet.ServerProxy.Services.ServersService;

namespace Tesonet.Client.Tests
{
    public abstract class BaseTests
    {
        protected readonly Mock<IMessengerService> _messengerServiceMock = new Mock<IMessengerService>();
        protected readonly Mock<IMessenger> _messengerMock = new Mock<IMessenger>();
        protected readonly Mock<IViewModelProvider> _viewModelProviderMock = new Mock<IViewModelProvider>();
        protected readonly Mock<INavigationService> _navigationServiceMock = new Mock<INavigationService>();
        protected readonly Mock<IAuthorizationService> _authorizationServiceMock = new Mock<IAuthorizationService>();
        protected readonly Mock<ISettings> _settingsServiceMock = new Mock<ISettings>();
        protected readonly Mock<IServersService> _serversServiceMock = new Mock<IServersService>();

        protected BaseTests()
        {
            _messengerServiceMock.Setup(x => x.Default).Returns(_messengerMock.Object);
        }
    }
}