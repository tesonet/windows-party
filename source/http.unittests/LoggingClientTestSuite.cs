using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.http.unittests
{
    [TestClass]
    public class LoggingClientTestSuite
    {
        [TestMethod]
        public void LoggingClient_Constructor_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var mockClient = new Mock<IClient>();

            var client = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingClient(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingClient(mockClient.Object, null));
            Assert.IsNotNull(new LoggingClient(mockClient.Object, mockLogger.Object));
        }

        [TestMethod]
        public async Task LoggingClient_GetJson_OnClientException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<ClientException>())).Callback(() => isCalled = true);
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new ClientException("Some exception"));
            var loggingClient = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<ClientException>(() => loggingClient.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingClient_GetJson_OnGeneralException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isCalled = true);
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingClient = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<Exception>(() => loggingClient.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingClient_GetJson_OnSuccess_ShouldLogInfo2Times()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var callCount = 0;

            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(() => callCount++);
            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Server[]>())).Callback(() => callCount++);
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Server[] { new Server { Name = "Lietuva"} });
            var loggingClient = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act
            var result = await loggingClient.GetJson<Server[]>(It.IsAny<string>(), It.IsAny<string>());

            // assert
            Assert.AreEqual(2, callCount);
        }

        [TestMethod]
        public async Task LoggingClient_PostJson_OnClientException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<ClientException>())).Callback(() => isCalled = true);
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ThrowsAsync(new ClientException("Some exception"));
            var loggingClient = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<ClientException>(() => loggingClient.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingClient_PostJson_OnGeneralException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isCalled = true);
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingClient = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<Exception>(() => loggingClient.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingClient_PostJson_OnSuccess_ShouldLogInfo2Times()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var callCount = 0;

            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Credentials>())).Callback(() => callCount++);
            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Credentials>(), It.IsAny<TokenResponse>())).Callback(() => callCount++);
            var mockClient = new Mock<IClient>();

            mockClient.Setup(c => c.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>())).ReturnsAsync(new TokenResponse { Token = "token" });
            var loggingClient = new LoggingClient(mockClient.Object, mockLogger.Object);

            // act
            var result = await loggingClient.PostJson<Credentials, TokenResponse>(It.IsAny<string>(), It.IsAny<Credentials>());

            // assert
            Assert.AreEqual(2, callCount);
        }
    }
}
