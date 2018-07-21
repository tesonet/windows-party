using log4net;
using Prism.Logging;

namespace WinPartyArs.Common
{
    public class PrismLog4NetProxy : ILoggerFacade
    {
        private readonly ILog log4netLogger = LogManager.GetLogger(typeof(PrismLog4NetProxy));
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    log4netLogger.Debug(message);
                    break;
                case Category.Warn:
                    log4netLogger.Warn(message);
                    break;
                case Category.Exception:
                    log4netLogger.Error(message);
                    break;
                case Category.Info:
                    log4netLogger.Info(message);
                    break;
            }
        }
    }
}
