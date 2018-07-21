using System;
using NLog;
using Tesonet.Windows_party.Services.Interfaces;

namespace Tesonet.Windows_party.Services.Implementations
{
    public class FileLoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public FileLoggerService()
        {
            _logger = LogManager.GetCurrentClassLogger();            
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}