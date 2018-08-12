using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tesonet.Client.Messages;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels;

namespace Tesonet.Client.Tests.ViewModelsTests
{
    [TestClass]
    public class NavigationToolBarViewModelTests : BaseTests
    {
        [TestMethod]
        public void ShouldNavigateToServersPageAsync_WhenGotoServersPage()
        {
            //arrange
            var navigationToolBarViewModel = new NavigationToolBarViewModel(_navigationServiceMock.Object, _messengerServiceMock.Object);

            //act
            navigationToolBarViewModel.GotoServersPageCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x=>x.NavigateToServersPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNavigateToSettingsPageAsync_WhenGotoSettingsPage()
        {
            //arrange
            var navigationToolBarViewModel = new NavigationToolBarViewModel(_navigationServiceMock.Object, _messengerServiceMock.Object);

            //act
            navigationToolBarViewModel.GotoSettingsPageCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x => x.NavigateToSettingsPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNavigateToLoginPageAsync_WhenGotoLoginPage()
        {
            //arrange
            var navigationToolBarViewModel = new NavigationToolBarViewModel(_navigationServiceMock.Object, _messengerServiceMock.Object);

            //act
            navigationToolBarViewModel.GotoLoginPageCommand.Execute(null);

            //assert
            _navigationServiceMock.Verify(x => x.NavigateToLoginPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }

        [TestMethod]
        public void ShouldRegisterOnIsBusyChangedMessage_WhenInitialized()
        {
            //act
            var navigationToolBarViewModel = new NavigationToolBarViewModel(_navigationServiceMock.Object, _messengerServiceMock.Object);

            //assert
            _messengerMock.Verify(x => x.Register(It.IsAny<object>(), It.IsAny<Action<IsBusyChangedMessage>>()), Times.Once);
        }
    }
}