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

namespace WindowsParty.Tests
{
    [TestFixture]
    public class TokenServiceTests
    {
        private IFixture _fixture;
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public async Task GetToken_Ok()
        {
            var logger = _fixture.Freeze<IWindowsLogger>();
            var sut = new TokenService(logger);

            var token = await sut.GetToken();
            Mock.Get(logger).Verify(l => l.WriteInformation(It.IsAny<string>()), Times.Exactly(2));
            Assert.IsNotNull(token);
        }
    }
}
