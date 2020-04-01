using API.Communicator;
using API.Logger;

namespace API
{
    public static class Program
    {
        public static ILogger Logger { get; private set; }
        public static ICommunicator Communicator { get; private set; }

        public static void Main()
        {
            Logger = new FileLogger(LogLevel.Debug, "Logger.log", false);
            Logger.Info("Application started");

            Communicator = new HttpCommunicator(Logger);
        }
    }
}
