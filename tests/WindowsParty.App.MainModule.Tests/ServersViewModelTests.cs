using System;
using System.Collections.Generic;
using System.Linq;
using WindowsParty.Infrastructure;
using WindowsParty.Infrastructure.Communication;
using WindowsParty.Infrastructure.Domain;
using WindowsParty.Infrastructure.Navigation;
using MainModule;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Prism.Regions;

namespace WindowsParty.App.MainModule.Tests
{

    [TestFixture]
    public class ServersViewModelTests
    {
        private Mock<IRegionNavigationService> _dummyNavigationService;

        private Mock<INavigator> _navigatorMock;
        private ServersViewModel _sut;
        private Mock<IServerListProvider> _serverListProviderMock;

        [SetUp]
        public void Init()
        {
            _serverListProviderMock = new Mock<IServerListProvider>();
            _navigatorMock = new Mock<INavigator>();
            _sut = new ServersViewModel(_navigatorMock.Object, _serverListProviderMock.Object);
        }


        [Test]
        public void LogoutCommand_NavigatesToInitialView()
        {
            _sut.LogoutCommand.Execute(new { });

            _navigatorMock.Verify(t => t.GoTo(AppViews.InitialView, null));
        }

        [Test]
        public void Servers_RaisesPropertyChanged()
        {
            var wasRaised = false;
            _sut.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_sut.Servers))
                {
                    wasRaised = true;
                }
            };

            _sut.Servers = new List<Server>();

            Assert.True(wasRaised);
        }

        [Test, AutoData]
        public void OnNavigatedTo_FillsServerList(string token, IList<Server> expectedServers)
        {
            var trasformedExpectedServers =
                expectedServers.Select(s => new Server {Distance = $"{s.Distance} km", Name = s.Name});
            _serverListProviderMock.Setup(t => t.GetServers(token)).Returns(expectedServers);

            _sut.OnNavigatedTo(GetNavigationContextWithToken(token));

            CollectionAssert.AreEquivalent(trasformedExpectedServers, _sut.Servers);
        }

        private NavigationContext GetNavigationContextWithToken(string token)
        {
            _dummyNavigationService = new Mock<IRegionNavigationService>();
            _dummyNavigationService.Setup(s => s.Region).Returns(new Region());
            return new NavigationContext(_dummyNavigationService.Object,
                new Uri("http://localhost:6666/"), new NavigationParameters { { "token", token } });
        }
    }
}
