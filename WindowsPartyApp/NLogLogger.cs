using Caliburn.Micro;
using System;

namespace WindowsPartyApp
{
    public class NLogLogger : ILog
    {
        private readonly NLog.Logger _innerLogger;

        public NLogLogger(Type type)
        {
            _innerLogger = NLog.LogManager.GetLogger(type.Name);
        }

        public void Error(Exception exception)
        {
            _innerLogger.Error(exception, exception.Message);
        }
        public void Info(string format, params object[] args)
        {
            _innerLogger.Info(format, args);
        }
        public void Warn(string format, params object[] args)
        {
            _innerLogger.Warn(format, args);
        }
    }
}
