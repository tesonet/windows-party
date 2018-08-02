using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using tesonet.windowsparty.contracts;
using tesonet.windowsparty.logging;

namespace tesonet.windowsparty.data.unittests
{
    [TestClass]
    public class UnitTestSuite
    {
        [TestMethod]
        public void StartedFinishedLoggingQueryHandler_Constructor_Arguments_ShouldThrowArgumentNullExceptionIfInvalid_ShouldNotThrowIfValid()
        {
            // arrange
            var mockTokenHandler = new Mock<IQueryHandler<Credentials, string>>();
            var mockLogger = new Mock<ILogger>();

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() => new StartedFinishedLoggingQueryHandler<Credentials, string>(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new StartedFinishedLoggingQueryHandler<Credentials, string>(mockTokenHandler.Object, null));
            Assert.IsNotNull(new StartedFinishedLoggingQueryHandler<Credentials, string>(mockTokenHandler.Object, mockLogger.Object));
        }

        [TestMethod]
        public async Task StartedFinishedLoggingQueryHandler_Get_OnGeneralException_ShouldLogInfoAndError()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var isErrorCalled = false;
            var isInfoCalled = false;

            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<IQuery<Credentials>>())).Callback(() => isInfoCalled = true);
            mockLogger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<Exception>())).Callback(() => isErrorCalled = true);
            var mockTokenHandler = new Mock<IQueryHandler<Credentials, string>>();

            mockTokenHandler.Setup(h => h.Get(It.IsAny<IQuery<Credentials>>())).ThrowsAsync(new Exception("Fatal exception"));
            var loggingTokenHandler = new StartedFinishedLoggingQueryHandler<Credentials, string>(mockTokenHandler.Object, mockLogger.Object);

            // act and assert
            await Assert.ThrowsExceptionAsync<Exception>(() => loggingTokenHandler.Get(It.IsAny<IQuery<Credentials>>()));
            Assert.IsTrue(isInfoCalled);
            Assert.IsTrue(isErrorCalled);
        }

        [TestMethod]
        public async Task StartedFinishedLoggingQueryHandler_Get_OnSuccess_ShouldLogInfo2Times()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();
            var callCount = 0;

            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<IQuery<Credentials>>())).Callback(() => callCount++);
            mockLogger.Setup(l => l.Info(It.IsAny<string>(), It.IsAny<IQuery<Credentials>>(), It.IsAny<string>())).Callback(() => callCount++);
            var mockTokenHandler = new Mock<IQueryHandler<Credentials, string>>();

            mockTokenHandler.Setup(h => h.Get(It.IsAny<IQuery<Credentials>>())).ReturnsAsync(It.IsAny<string>());
            var loggingTokenHandler = new StartedFinishedLoggingQueryHandler<Credentials, string>(mockTokenHandler.Object, mockLogger.Object);

            // act
            var result = await loggingTokenHandler.Get(It.IsAny<IQuery<Credentials>>());

            // assert
            Assert.AreEqual(2, callCount);
        }
    }
}
