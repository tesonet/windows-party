using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;
using tesonet.windowsparty.services.Authentication;
using tesonet.windowsparty.services.Servers;

namespace tesonet.windowsparty.services.unittests
{
    [TestClass]
    public class LoggingTestSuite
    {
        [TestMethod]
        public void LoggingAuthenticationService_Constructor_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var mockAuthenticationService = new Mock<IAuthenticationService>();

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingAuthenticationService(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingAuthenticationService(mockAuthenticationService.Object, null));
            Assert.IsNotNull(new LoggingAuthenticationService(mockAuthenticationService.Object, mockLogger.Object));
        }

        [TestMethod]
        public void LoggingServersService_Constructor_InvalidArguments_ShouldThrowArgumentExceptions()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var mockServersService = new Mock<IServersService>();

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingServersService(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new LoggingServersService(mockServersService.Object, null));
            Assert.IsNotNull(new LoggingServersService(mockServersService.Object, mockLogger.Object));
        }

        [TestMethod]
        public async Task LoggingAuthenticationService_Authentication_OnAuthenticationServiceException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;
            var credentials = new Credentials { Username = "some", Password = "some" };

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<AuthenticationServiceException>())).Callback(() => isCalled = true);
            var mockAuthenticationService = new Mock<IAuthenticationService>();

            mockAuthenticationService.Setup(a => a.Authenticate(It.IsAny<Credentials>())).ThrowsAsync(new AuthenticationServiceException("Some exception"));
            var loggingAuthenticationService = new LoggingAuthenticationService(mockAuthenticationService.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<AuthenticationServiceException>(() => loggingAuthenticationService.Authenticate(credentials));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingServersService_Get_OnServersServiceException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<ServersServiceException>())).Callback(() => isCalled = true);
            var mockServersService = new Mock<IServersService>();

            mockServersService.Setup(s => s.Get(It.IsAny<string>())).ThrowsAsync(new ServersServiceException("Some exception"));
            var loggingServersService = new LoggingServersService(mockServersService.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<ServersServiceException>(() => loggingServersService.Get(It.IsAny<string>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingAuthenticationService_Authentication_OnException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;
            var credentials = new Credentials { Username = "some", Password = "some" };

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isCalled = true);
            var mockAuthenticationService = new Mock<IAuthenticationService>();

            mockAuthenticationService.Setup(a => a.Authenticate(It.IsAny<Credentials>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingAuthenticationService = new LoggingAuthenticationService(mockAuthenticationService.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<Exception>(() => loggingAuthenticationService.Authenticate(credentials));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingServersService_Get_OnException_ShouldLogError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isCalled = false;

            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isCalled = true);
            var mockServersService = new Mock<IServersService>();

            mockServersService.Setup(s => s.Get(It.IsAny<string>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingServersService = new LoggingServersService(mockServersService.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<Exception>(() => loggingServersService.Get(It.IsAny<string>()));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task LoggingAuthenticationService_Authentication_OnSuccess_ShouldLogInfo2Times()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var callCount = 0;
            var credentials = new Credentials { Username = "some", Password = "some" };

            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>())).Callback(() => callCount++);
            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(() => callCount++);
            var mockAuthenticationService = new Mock<IAuthenticationService>();

            mockAuthenticationService.Setup(a => a.Authenticate(It.IsAny<Credentials>())).ReturnsAsync("token");
            var loggingAuthenticationService = new LoggingAuthenticationService(mockAuthenticationService.Object, mockLogger.Object);

            // act
            var result = await loggingAuthenticationService.Authenticate(credentials);

            // assert
            Assert.AreEqual(2, callCount);
        }

        [TestMethod]
        public async Task LoggingServersService_Get_OnSuccess_ShouldLogInfo2Times()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var callCount = 0;

            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>())).Callback(() => callCount++);
            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Server[]>())).Callback(() => callCount++);
            var mockServersService = new Mock<IServersService>();

            mockServersService.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(new Server[] { new Server { Name = "Lietuva" } });
            var loggingServersService = new LoggingServersService(mockServersService.Object, mockLogger.Object);

            // act
            var result = await loggingServersService.Get(It.IsAny<string>());

            // assert
            Assert.AreEqual(2, callCount);
        }
    }
}
