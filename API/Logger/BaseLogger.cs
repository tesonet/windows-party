using System;
namespace API.Logger
{
    abstract class BaseLogger : ILogger
    {
        private LogLevel level;

        public LogLevel Level
        {
            get { return level; }
            set { level = value; }
        }

        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void Log(LogLevel level, string message)
        {
            if (Level >= level)
            {
                string output = FormatLogEntry(level, message);
                Output(output);
            }
        }

        public abstract void Output(string message);

        string FormatLogEntry(LogLevel level, string message)
        {
            return "[" + DateTime.UtcNow + "] " + Enum.GetName(typeof(LogLevel), level).ToUpper() + ": " + message;
        }
    }
}
