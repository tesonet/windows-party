using System;

namespace Tesonet.Windows_party.Services.Interfaces
{
    public interface ILoggerService
    {
        void Error(Exception ex);

        void Error(string message);

        void Info(string message);        
    }
}
