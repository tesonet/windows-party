using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsParty.ViewModels;
using Moq;
using WindowsParty.Services.Contracts;
using log4net;
using WindowsParty.Clients.Contracts;
using Caliburn.Micro;
using WindowsParty.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WindowsParty.Tests.ViewModels
{
    [TestClass]
    public class ServerListViewModelTests
    {
        private class ServerListViewModel_Exposer : ServerListViewModel
        {
            public ServerListViewModel_Exposer(ISessionService sessionService, IEventAggregator eventAggregator, IApiClient apiClient) : base(sessionService, eventAggregator, apiClient)
            {
            }

            public async Task OnActivate_Exposed()
            {
                OnActivate();
                //Delay just in case. OnActivate is async void method and can not be awaited. Asserts will fail if they start first.
                await Task.Delay(100);
            }
        }

        [TestMethod]
        public async Task Logout()
        {
            //Setup
            var sessionMock = new Mock<ISessionService>();
            var eventAggMock = new Mock<IEventAggregator>();
            var clientMock = new Mock<IApiClient>();
            var model = new ServerListViewModel(sessionMock.Object, eventAggMock.Object, clientMock.Object);

            //Act
            model.Servers = new BindableCollection<Server>();
            model.Logout();

            //Assert
            sessionMock.Verify(m => m.Logout(), Times.Once);
            Assert.IsNull(model.Servers);
        }

        [TestMethod]
        public async Task OnActivate()
        {
            //Setup
            var sessionMock = new Mock<ISessionService>();
            sessionMock.Setup(m => m.GetToken()).ReturnsAsync("abc123");
            var eventAggMock = new Mock<IEventAggregator>();
            var clientMock = new Mock<IApiClient>();
            clientMock.Setup(m => m.GetServers(It.Is<string>(v => v == "abc123"))).ReturnsAsync(
                new RestResponse<List<Server>>
                {
                    Data = new List<Server>()
                    {
                        new Server(),
                        new Server()
                    }
                }
            );
            var model = new ServerListViewModel_Exposer(sessionMock.Object, eventAggMock.Object, clientMock.Object);

            //Act
            await model.OnActivate_Exposed();

            //Assert
            Assert.IsNotNull(model.Servers);
            Assert.AreEqual(2, model.Servers.Count);
        }
    }
}
