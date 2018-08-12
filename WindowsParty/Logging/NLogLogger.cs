using Caliburn.Micro;
using NLog;
using System;
using EnsureThat;

namespace WindowsParty.Logging
{
	public class NLogLogger : ILog, ILogger
	{
		private readonly Logger _innerLogger;

		public NLogLogger(Type type)
		{
			EnsureArg.IsNotNull(type, nameof(type));

			_innerLogger = NLog.LogManager.GetLogger(type.Name);
		}

		public NLogLogger(string name)
		{
			EnsureArg.IsNotNullOrEmpty(name, nameof(name));

			_innerLogger = NLog.LogManager.GetLogger(name);
		}

		#region Caliburn.Micro.ILog members

		public void Error(Exception exception) => _innerLogger.Error(exception, exception.Message);

		public void Warn(string format, params object[] args) => _innerLogger.Warn(format, args);

		#endregion

		#region WindowsParty.Logging.ILogger members

		public void Debug(string message, params object[] args) => _innerLogger.Debug(message, args);

		public void Warning(string message, params object[] args) => _innerLogger.Warn(message, args);

		public void Trace(string message, params object[] args) => _innerLogger.Trace(message, args);

		public void Error(string message, params object[] args) => _innerLogger.Error(message, args);

		#endregion

		#region Common members

		public void Info(string format, params object[] args) => _innerLogger.Info(format, args);

		#endregion
	}
}