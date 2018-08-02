using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.data.Servers;
using tesonet.windowsparty.data.Tokens;
using tesonet.windowsparty.http;

namespace tesonet.windowsparty.data.unittests.Quering
{
    [TestClass]
    public class UnitTestSuite
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TokenQueryHandler_Constructor_UrlIsNull_ShouldThrowArgumentNullException()
        {
            // arrange and act
            var tokenHandler = new TokenQueryHandler(null);

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ServerQueryHandler_Constructor_UrlIsNull_ShouldThrowArgumentNullException()
        {
            // arrange and act
            var serverHandler = new ServerQueryHandler(null);

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TokenQueryHandler_Get_QueryIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ReturnsAsync(new TokenResponse { Token = "token" });

            var tokenHandler = new TokenQueryHandler(mockClient.Object);

            // act
            var result = await tokenHandler.Get(null);

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ServerQueryHandler_Get_QueryIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ReturnsAsync(new TokenResponse { Token = "token" });

            var serverHandler = new ServerQueryHandler(mockClient.Object);

            // act
            var result = await serverHandler.Get(null);

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TokenQueryHandler_Get_QueryPayloadIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ReturnsAsync(new TokenResponse { Token = "token" });

            var tokenHandler = new TokenQueryHandler(mockClient.Object);

            // act
            var result = await tokenHandler.Get(new TokenQuery());

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ServerQueryHandler_Get_QueryPayloadIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Server[] { new Server { Name = "Lietuva" } });

            var tokenHandler = new ServerQueryHandler(mockClient.Object);

            // act
            var result = await tokenHandler.Get(new ServerQuery());

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(TokenQueryException))]
        public async Task TokenQueryHandler_Get_OnException_ShouldThrowTokenQueryException()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ThrowsAsync(new ClientException("some error"));

            var tokenHandler = new TokenQueryHandler(mockClient.Object);

            // act
            var result = await tokenHandler.Get(
                new TokenQuery {
                    Payload = new Credentials
                    {
                        Username = "username",
                        Password = "password"
                    }
                });

            // assert ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ServerQueryException))]
        public async Task ServerQueryHandler_Get_OnException_ShouldThrowTokenQueryException()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new ClientException("some error"));

            var serverHandler = new ServerQueryHandler(mockClient.Object);

            // act
            var result = await serverHandler.Get(new ServerQuery { Payload = "token" });

            // assert ExpectedException
        }

        [TestMethod]
        public async Task TokenQueryHandler_Get_ShouldReturnValidResult()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ReturnsAsync(new TokenResponse { Token = "token" });

            var tokenHandler = new TokenQueryHandler(mockClient.Object);

            // act
            var result = await tokenHandler.Get(
                new TokenQuery
                {
                    Payload = new Credentials
                    {
                        Username = "username",
                        Password = "password"
                    }
                });

            // assert
            Assert.AreEqual("token", result.Token);
        }

        [TestMethod]
        public async Task ServerQueryHandler_Get_ShouldReturnValidResult()
        {
            // arrange
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Server[] { new Server { Name = "Lietuva" } });

            var serverHandler = new ServerQueryHandler(mockClient.Object);

            // act
            var result = await serverHandler.Get(new ServerQuery { Payload = "token" });

            // assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Lietuva", result[0].Name);
        }
    }
}
