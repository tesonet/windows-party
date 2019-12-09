using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;

namespace WindowsParty.Logger.Loggers
{
    public interface IWindowsLogger
    {
        void WriteError(string message, Exception ex);
        void WriteInformation(string message);
    }
}
