using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ISerilogLogger = Serilog.ILogger;

namespace tesonet.windowsparty.logging.unittests
{
    [TestClass]
    public class UnitTestSuite
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Logger_Constructor_LoggerIsNull_ShouldThrowArgumentNullException()
        {
            // arrange, act
            var logger = new Logger(null);

            // assert ExpectedException
        }

        [TestMethod]
        public void Logger_Info_ShouldForwardsMessageToInnerLogger()
        {
            // arrange
            var callCount = 0;
            var message = "message";
            var mockSerilogLogger = new Mock<ISerilogLogger>();

            mockSerilogLogger
                .Setup(l => l.Information(It.Is<string>(s => s == message), It.IsAny<string>()))
                .Callback(() => callCount++);
            mockSerilogLogger
                .Setup(l => l.Information(It.Is<string>(s => s == message), It.IsAny<string>(), It.IsAny<string>()))
                .Callback(() => callCount++);
            mockSerilogLogger
                .Setup(l => l.Information(It.Is<string>(s => s == message), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback(() => callCount++);

            var logger = new Logger(mockSerilogLogger.Object);

            // act
            logger.Info(message, "property0");
            logger.Info(message, "property0", "property1");
            logger.Info(message, "property0", "property1", "property2");

            // assert
            Assert.AreEqual(3, callCount);
        }

        [TestMethod]
        public void Logger_Error_ShouldForwardsMessageToInnerLogger()
        {
            // arrange
            var isCalled = false;
            var error = "error";
            var mockSerilogLogger = new Mock<ISerilogLogger>();

            mockSerilogLogger.Setup(l => l.Error(It.Is<string>((s) => s == error), It.IsAny<Exception>())).Callback(() => isCalled = true);

            var logger = new Logger(mockSerilogLogger.Object);

            // act
            logger.Error(error, new Exception(error));

            // assert
            Assert.IsTrue(isCalled);
        }
    }
}
