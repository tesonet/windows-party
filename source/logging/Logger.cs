using EnsureThat;
using ISerilogLogger = Serilog.ILogger;

namespace tesonet.windowsparty.logging
{
    public class Logger : ILogger
    {
        private readonly ISerilogLogger _logger;

        public Logger(ISerilogLogger logger)
        {
            Ensure.That(logger, nameof(logger)).IsNotNull();

            _logger = logger;
        }

        public void Info<T0>(string message, T0 propertyValue0)
        {
            _logger.Information(message, propertyValue0);
        }

        public void Info<T0, T1>(string message, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Information(message, propertyValue0, propertyValue1);
        }

        public void Info<T0, T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Information(message, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error<T0>(string message, T0 propertyValue0)
        {
            _logger.Error(message, propertyValue0);
        }
    }
}
