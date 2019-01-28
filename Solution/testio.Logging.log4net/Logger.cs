using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testio.Core.Logging;
using l4n = log4net;

namespace testio.Logging.log4net
{
    public class Logger : ILogger
    {
        #region Fields

        private l4n.ILog _iLog = null;

        #endregion Fields

        #region Constructors

        public Logger()
        {
            _iLog = l4n.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion Constructors

        #region ILogger

        void ILogger.LogInfoFormat(string message, params object[] parameters)
        {
            _iLog.InfoFormat(message, parameters);
        }

        void ILogger.LogWarningFormat(string message, params object[] parameters)
        {
            _iLog.WarnFormat(message, parameters);
        }

        void ILogger.LogErrorFormat(Exception e, string message, params object[] parameters)
        {
            message = String.Format(message, parameters);
            message = String.Format("{0}: {1}", message, e.Message);
            _iLog.Error(message);
        }

        #endregion ILogger
    }
}