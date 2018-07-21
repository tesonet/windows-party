using Prism.Logging;
using System;

namespace WinPartyArs.Common
{
    public static class StaticHelpers
    {
        public static Exception GetInnerMostException(Exception ex)
        {
            if (null == ex)
                throw new ArgumentNullException(nameof(ex));
            while (null != ex.InnerException)
                ex = ex.InnerException;
            return ex;
        }

        public static void Log(this ILoggerFacade log, string message, Category category) => log.Log(message, category, Priority.None);
    }
}
