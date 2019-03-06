using log4net;

namespace TheHaveFunApp.Services.Interfaces
{
    public interface ILogService
    {
        void Init(ILog log);
        void LogEvent(string eventName);
        void LogException(object exception);
    }
}
