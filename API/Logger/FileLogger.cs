using System;
using System.IO;

namespace API.Logger
{
    class FileLogger : BaseLogger, ILogger, IDisposable
    {
        string filename;
        TextWriter writer;

        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        FileLogger(LogLevel level)
        {
            Level = level;
        }

        public FileLogger() : this(LogLevel.Error) { }

        public FileLogger(LogLevel level, string fileName) : this(level, fileName, false) { }

        public FileLogger(LogLevel level, string fileName, bool append) : this(level)
        {
            Filename = fileName;
            writer = TextWriter.Synchronized(new StreamWriter(Filename, append));
        }

        public override void Output(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void Dispose()
        {
            writer.Flush();
            ((IDisposable)writer).Dispose();
        }
    }
}
