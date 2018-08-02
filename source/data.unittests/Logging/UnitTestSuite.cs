using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;
using tesonet.windowsparty.data.Servers;
using tesonet.windowsparty.data.Tokens;

namespace tesonet.windowsparty.data.unittests.Logging
{
    [TestClass]
    public class UnitTestSuite
    {
        [TestMethod]
        public void LoggingTokenQueryHandler_Constructor_Arguments_ShouldThrowArgumentNullExceptionIfInvalid_ShouldNotThrowIfValid()
        {
            // arrange
            var mockTokenHandler = new Mock<IQueryHandler<Credentials, TokenResponse>>();
            var mockLogger = new Mock<ILogger>();

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingTokenQueryHandler(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingTokenQueryHandler(mockTokenHandler.Object, null));
            Assert.IsNotNull(new LoggingTokenQueryHandler(mockTokenHandler.Object, mockLogger.Object));
        }

        [TestMethod]
        public void LoggingServerQueryHandler_Constructor_Arguments_ShouldThrowArgumentNullExceptionIfInvalid_ShouldNotThrowIfValid()
        {
            // arrange
            var mockServerHandler = new Mock<IQueryHandler<string, Server[]>>();
            var mockLogger = new Mock<ILogger>();

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingServerQueryHandler(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingServerQueryHandler(mockServerHandler.Object, null));
            Assert.IsNotNull(new LoggingServerQueryHandler(mockServerHandler.Object, mockLogger.Object));
        }

        [TestMethod]
        public async Task LoggingTokenQueryHandler_Get_OnTokenQueryException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<TokenQueryException>())).Callback(() => isCalled = true);
            var mockTokenHandler = new Mock<IQueryHandler<Credentials, TokenResponse>>();

            mockTokenHandler.Setup(h => h.Get(It.IsAny<IQuery<Credentials>>())).ThrowsAsync(new TokenQueryException("Some exception"));
            var loggingTokenHandler = new LoggingTokenQueryHandler(mockTokenHandler.Object, mockLogger.Object);

            await Assert.ThrowsExceptionAsync<TokenQueryException>(() => loggingTokenHandler.Get(It.IsAny<IQuery<Credentials>>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingServerQueryHandler_Get_OnServerQueryException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<ServerQueryException>())).Callback(() => isCalled = true);
            var mockServerHandler = new Mock<IQueryHandler<string, Server[]>>();

            mockServerHandler.Setup(h => h.Get(It.IsAny<IQuery<string>>())).ThrowsAsync(new ServerQueryException("Some exception"));
            var loggingServerHandler = new LoggingServerQueryHandler(mockServerHandler.Object, mockLogger.Object);

            await Assert.ThrowsExceptionAsync<ServerQueryException>(() => loggingServerHandler.Get(It.IsAny<IQuery<string>>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingTokenQueryHandler_Get_OnGeneralException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isCalled = true);
            var mockTokenHandler = new Mock<IQueryHandler<Credentials, TokenResponse>>();

            mockTokenHandler.Setup(h => h.Get(It.IsAny<IQuery<Credentials>>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingTokenHandler = new LoggingTokenQueryHandler(mockTokenHandler.Object, mockLogger.Object);

            await Assert.ThrowsExceptionAsync<Exception>(() => loggingTokenHandler.Get(It.IsAny<IQuery<Credentials>>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingServerQueryHandler_Get_OnGeneralException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isCalled = true);
            var mockServerHandler = new Mock<IQueryHandler<string, Server[]>>();

            mockServerHandler.Setup(h => h.Get(It.IsAny<IQuery<string>>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingTokenHandler = new LoggingServerQueryHandler(mockServerHandler.Object, mockLogger.Object);

            await Assert.ThrowsExceptionAsync<Exception>(() => loggingTokenHandler.Get(It.IsAny<IQuery<string>>()));
            Assert.IsTrue(isCalled);
        }
    }
}
