using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using WindowsParty.Interfaces;
using Caliburn.Micro;
using WindowsParty.ViewModels;
using WindowsParty.Models;
using AutoFixture;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using System.Collections.Generic;

namespace WindowsParty.Tests
{
    [TestClass]
    public class ServerViewTests
    {
        private Mock<IWebTasks> _webTasks;
        private Mock<IAuthenticationHelper> _authenticationHelper;
        private Mock<IEventAggregator> _eventAggregator;
        private AuthModel _authModel;
        private List<ServerModel> _serverList;
        private ServerListViewModel _serverListViewModel;

        [TestInitialize]
        public void Setup()
        {
            _webTasks = new Mock<IWebTasks>();
            _authenticationHelper = new Mock<IAuthenticationHelper>();
            _eventAggregator = new Mock<IEventAggregator>();

            _authModel = new AuthModel()
            {
                AuthToken = "f9731b590611a5a9377fbd02f247fcdf"
            };

            _serverList = new List<ServerModel>
            {
                new ServerModel
                {
                    Distance = 1000,
                    ServerName = "TestServer"
                },
                new ServerModel
                {
                    Distance = 2000,
                    ServerName = "TestServer2"
                }
            };

            _authenticationHelper.Setup(x => x.AuthenticateUser(It.IsAny<UserModel>())).ReturnsAsync(_authModel);
            _webTasks.Setup(x => x.RetrieveServerList(It.IsAny<AuthModel>())).ReturnsAsync(_serverList);

            _serverListViewModel = new ServerListViewModel(_eventAggregator.Object, _authenticationHelper.Object, _webTasks.Object);
        }

        [TestMethod]
        public void ServerListIsMapped()
        {
            var fixture = new Fixture();

            List<ServerModel> _serverList = fixture.Create<List<ServerModel>>();

            _serverListViewModel.AddServers(_serverList);

            Assert.AreEqual(_serverList, _serverListViewModel.Servers);
        }

        [TestMethod]
        public async Task RetrieveServerListIsCalled()
        {
            var fixture = new Fixture();

            await _serverListViewModel.RetrieveServerList();

            _webTasks.Verify(x => x.RetrieveServerList(It.IsAny<AuthModel>()));
        }

        [TestMethod]
        public async Task RetrievedServerListIsMapped()
        {
            var fixture = new Fixture();

            await _serverListViewModel.RetrieveServerList();

            Assert.AreEqual(_serverList, _serverListViewModel.Servers);
        }
    }
}