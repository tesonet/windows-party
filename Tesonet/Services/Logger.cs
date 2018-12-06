namespace Tesonet.Services
{
    using System;
    using Tesonet.Services;
    public class Logger : ILogger
    {
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            var logger = NLog.LogManager.GetLogger("General");
            logger.Error(message);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="format">The format specification.</param>
        /// <param name="args">The arguments for the format specification.</param>
        public void Error(Exception exception, string format, params object[] args)
        {
            var logger = NLog.LogManager.GetLogger("General");
            logger.Error(exception, format, args);
        }
    }
}