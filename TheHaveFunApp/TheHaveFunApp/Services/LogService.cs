using System;
using log4net;
using TheHaveFunApp.Services.Interfaces;

namespace TheHaveFunApp.Services
{
    public class LogService : ILogService
    {
        private ILog _log;

        public void Init(ILog log)
        {
            _log = log;
        }

        public void LogEvent(string eventName)
        {
            _log.Info(eventName);
        }

        public void LogException(object exception)
        {
            _log.Error(exception);
        }
    }
}
