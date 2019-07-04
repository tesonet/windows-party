using Caliburn.Micro;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesonetWinParty.Helpers;
using TesonetWinParty.Models;
using TesonetWinParty.ViewModels;

namespace TesonetWinParty.Tests
{

    [TestFixture]
    public class ServersListViewTest
    {
        private const string _userName = "tesonet";
        private const string _password = "partyanimal";
        Mock<IAccountHelper> _accountHelper;
        Mock<IEventAggregator> _eventAggregator;
        Mock<IAPIHelper> _apiHelper;

        ServersViewModel _serversVm;

        [SetUp]
        public void Setup()
        {
            _accountHelper = new Mock<IAccountHelper>();
            _accountHelper.SetupGet(x => x.Token).Returns(new TokenItem());
            
            _apiHelper = new Mock<IAPIHelper>();
            _apiHelper.CallBase = true;
            _apiHelper.Setup(x => x.GetServersList(It.IsAny<string>())).ReturnsAsync(new List<Server>(){ new Server {Name = "name", Distance = 111 }});

            _eventAggregator = new Mock<IEventAggregator>();
            _serversVm = new ServersViewModel(_apiHelper.Object, _eventAggregator.Object, _accountHelper.Object);
        }

        [Test]
        public async Task Test_GetAndSetServersList()
        {
            await _serversVm.LoadServers();
            Assert.NotZero(_serversVm.Servers.Count, "Error while addind servers list");
        }
    }
}
