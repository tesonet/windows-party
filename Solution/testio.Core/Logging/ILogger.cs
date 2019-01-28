using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testio.Core.Logging
{
    public interface ILogger
    {
        void LogInfoFormat(string message, params object[] parameters);
        void LogWarningFormat(string message, params object[] parameters);
        void LogErrorFormat(Exception e, string message, params object[] parameters);
    }
}
