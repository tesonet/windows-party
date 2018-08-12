using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tesonet.Client.Services.NavigationService.NavigationData;
using Tesonet.Client.ViewModels;
using Tesonet.Domain.Domain;

namespace Tesonet.Client.Tests.ViewModelsTests
{
    [TestClass]
    public class ServersPageViewModelTests : BaseTests
    {
        [TestMethod]
        public async Task ShouldFillServersList_WhenInitializeAsync()
        {
            //arrange
            var servers = new ObservableCollection<Server>
            {
                new Server {Country = "Ukraine", Distance = 1000, Number = 1},
                new Server {Country = "Ukraine", Distance = 500, Number = 2},
                new Server {Country = "Italy", Distance = 500, Number = 1},
            };

            _serversServiceMock.Setup(x => x.GetServersAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(servers));
            var serversPageViewModel = new ServersPageViewModel(_navigationServiceMock.Object,
                _serversServiceMock.Object, _settingsServiceMock.Object);

            //act
            await serversPageViewModel.InitializeAsync(null);

            //assert
            CollectionAssert.AreEqual(servers, serversPageViewModel.Servers);
        }

        [TestMethod]
        public async Task ShouldNavigateToErrorPageAsync_WhenInitializeAsyncThroException()
        {
            //arrange
            _serversServiceMock.Setup(x => x.GetServersAsync(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
            var serversPageViewModel = new ServersPageViewModel(_navigationServiceMock.Object,
                _serversServiceMock.Object, _settingsServiceMock.Object);

            //act
            await serversPageViewModel.InitializeAsync(null);

            //assert
            _navigationServiceMock.Verify(x=>x.NavigateToErrorPageAsync(It.IsAny<NavigationData>()), Times.Once);
        }
    }
}