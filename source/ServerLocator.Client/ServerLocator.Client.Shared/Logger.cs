using log4net;
using System;

namespace ServerLocator.Client.Shared
{
    public class Logger : Caliburn.Micro.ILog
    {
        private readonly ILog log;

        public Logger()
        {
            log = LogManager.GetLogger(typeof(Logger));
        }

        public void Error(Exception exception)
        {
            log.Error("Exception", exception);
        }

        public void Info(string format, params object[] args)
        {
            log.Info(String.Format(format, args));
        }

        public void Warn(string format, params object[] args)
        {
            log.Warn(String.Format(format, args));
        }
    }
}
