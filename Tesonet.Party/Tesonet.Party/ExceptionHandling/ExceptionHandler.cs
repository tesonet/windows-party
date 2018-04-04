using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet.Party.ExceptionHandling
{
    public static class ExceptionHandler
    {
        public static void HandleException(Exception ex, bool logException)
        {
            var report = GenerateReport(ex);
            if (logException)
                LogException(report);
            new ErrorWindow(report).ShowDialog();
        }

        public static void LogException(Exception ex)
        {
            LogException(GenerateReport(ex));
        }

        public static void LogException(string message)
        {
            // todo: add exception loging
        }

        public static string GenerateReport(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.AppendFormat("Application version: {0}", assembly.FullName);
            builder.AppendLine();

            AssemblyFileVersionAttribute attribute = (AssemblyFileVersionAttribute)assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true).SingleOrDefault();
            if (attribute != null)
            {
                builder.AppendFormat("Current File Version: {0}", attribute.Version);
                builder.AppendLine();
            }

            builder.AppendFormat("Report UTC date: {0}", DateTime.UtcNow);
            builder.AppendLine();
            builder.AppendFormat("OS: {0}", Environment.OSVersion.Version);
            builder.AppendLine();
            builder.AppendLine();

            if (ex != null)
            {
                builder.AppendLine("Exception: ");
                builder.Append(ExceptionHierarchyToString(ex));
            }

            return builder.ToString();
        }

        private static string ExceptionHierarchyToString(Exception exception)
        {
            var currentException = exception;
            var stringBuilder = new StringBuilder();
            var count = 0;

            while (currentException != null)
            {
                if (count++ == 0)
                    stringBuilder.AppendLine("Top-level Exception");
                else
                    stringBuilder.AppendLine("Inner Exception " + (count - 1));

                stringBuilder.AppendLine("Type:        " + currentException.GetType())
                             .AppendLine("Message:     " + currentException.Message);

                if (currentException.StackTrace != null)
                    stringBuilder.AppendLine("Stack Trace: " + currentException.StackTrace.Trim());

                stringBuilder.AppendLine();
                currentException = currentException.InnerException;
            }

            var exceptionString = stringBuilder.ToString();
            return exceptionString.TrimEnd();
        }
    }
}
