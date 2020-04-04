using Caliburn.Micro;
using System;

namespace WPFApp.Services
{
   public class LoggerService: ILog
    {

            private readonly log4net.ILog _innerLogger;

            public LoggerService(Type type)
            {
                _innerLogger = log4net.LogManager.GetLogger(type);
            }

            public void Error(Exception exception)
            {
                _innerLogger.Error(exception.Message, exception);
            }

            public void Info(string format, params object[] args)
            {
                _innerLogger.InfoFormat(format, args);
            }

            public void Warn(string format, params object[] args)
            {
                _innerLogger.WarnFormat(format, args);
            }
        }
}

