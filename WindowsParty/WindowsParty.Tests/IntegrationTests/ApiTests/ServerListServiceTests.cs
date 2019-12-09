using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Api;
using WindowsParty.Logger.Loggers;

namespace WindowsParty.Tests.IntegrationTests.ApiTests
{
    [TestFixture]
    public class ServerListServiceTests
    {
        private IFixture _fixture;
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public async Task GetServerList_Ok()
        {
            var logger = _fixture.Freeze<IWindowsLogger>();
            var tokenService = new TokenService(logger);

            var sut = new ServerListService(logger, tokenService);

            var token = await sut.GetServerList();
            Mock.Get(logger).Verify(l => l.WriteInformation(It.IsAny<string>()), Times.Exactly(4));
            Assert.IsNotNull(token);
        }

        [Test]
        public async Task GetServerList_NullResponse()
        {
            var logger = _fixture.Freeze<IWindowsLogger>();
            var tokenService = _fixture.Freeze<ITokenService>();

            var sut = new ServerListService(logger, tokenService);

            var serverList = await sut.GetServerList();

            Mock.Get(logger).Verify(l => l.WriteInformation(It.IsAny<string>()), Times.Exactly(2));
            Assert.IsNull(serverList);
        }
    }
}
