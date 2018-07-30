using System;

namespace WindowsParty.Common.Interfaces
{
    /// <summary>
    /// Service logs ex and info messages
    /// </summary>
    public interface ILogService
    {
        void Error(Exception ex);
        void Info(string message);
    }
}
