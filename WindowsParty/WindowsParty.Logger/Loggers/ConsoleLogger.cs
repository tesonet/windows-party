using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;

namespace WindowsParty.Logger.Loggers
{
    public class ConsoleLogger : IWindowsLogger
    {
        private readonly ILogger _logger;
        public ConsoleLogger()
        {
            _logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }
        public void WriteError(string message, Exception ex)
        {
            var log = GetWpfLogEntry(message, ex);
            if(ex != null)
            {
                log.Message = GetMessageFromException(log.Exception);
            }
            _logger.Write(Serilog.Events.LogEventLevel.Error, "{@Log}", log);
        }

        public void WriteInformation(string message)
        {
            var log = GetWpfLogEntry(message);
            _logger.Write(Serilog.Events.LogEventLevel.Information, "{@Log}", log);
        }

        private LogDetail GetWpfLogEntry(string message, Exception ex = null)
        {
            return new LogDetail()
            {
                Timestamp = DateTime.Now,
                UserName = Environment.UserName,
                Exception = ex
            };
        }

        private string GetMessageFromException(Exception ex)
        {
            if(ex.InnerException != null)
            {
                return GetMessageFromException(ex.InnerException);
            }
            return ex.Message;
        }
    }
}
