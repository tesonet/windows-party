using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels;

namespace Tesonet.Client.Tests.ViewModelsTests
{
    [TestClass]
    public class LoginViewModelTests : BaseTests
    {
        [TestMethod]
        public void ShouldGetAuthToken_WhenLogin()
        {
            //arrange
            _authorizationServiceMock.Setup(x => x.GetAuthToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(string.Empty));
            var loginViewModel = new LoginViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);

            //act
            loginViewModel.LoginCommand.Execute(null);

            //assert
            _authorizationServiceMock.Verify(x=>x.GetAuthToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNavigateToErrorPage_WhenLoginAndAuthTokenIsEmpty()
        {
            //arrange
            _authorizationServiceMock.Setup(x => x.GetAuthToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(string.Empty));
            var loginViewModel = new LoginViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);

            //act
            loginViewModel.LoginCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x => x.NavigateToErrorPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNavigateToErrorPage_WhenLoginAndGetAuthTokenThrowedException()
        {
            //arrange
            _authorizationServiceMock
                .Setup(x => x.GetAuthToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();
            var loginViewModel = new LoginViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);

            //act
            loginViewModel.LoginCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x => x.NavigateToErrorPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNavigateToServersPageAsync_WhenLoginAndGetAuthTokenVerified()
        {
            //arrange
            _authorizationServiceMock
                .Setup(x => x.GetAuthToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult("validtoken"));
            var loginViewModel = new LoginViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);

            //act
            loginViewModel.LoginCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x => x.NavigateToServersPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNavigateToSettingsPageAsyncc_WhenGotoSettings()
        {
            //arrange
            var loginViewModel = new LoginViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);

            //act
            loginViewModel.GotoSettingsCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x => x.NavigateToSettingsPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldSetAuthTokenToEmptyString_WhenGotoSettings()
        {
            //arrange
            _settingsServiceMock.Setup(x => x.AuthToken).Returns("ValidToken");
            var loginViewModel = new LoginViewModel(_navigationServiceMock.Object, _authorizationServiceMock.Object, _settingsServiceMock.Object);

            //act
            loginViewModel.GotoSettingsCommand.Execute(null);

            //assert
            _settingsServiceMock.VerifySet(x=>x.AuthToken = string.Empty);
        }
    }
}