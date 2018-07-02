using NLog;
using System;
using WindowsParty.Common.Interfaces;

namespace WindowsParty.Services
{
    public class FileNLogService : ILogService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Error(Exception ex)
        {
            Logger.Error(ex);
        }

        public void Info(string message)
        {
           Logger.Info(message);
        }
    }
}
