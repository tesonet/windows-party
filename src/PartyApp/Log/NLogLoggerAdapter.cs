using System;
using NLog;
using PartyApp.Utilities;

namespace PartyApp.Log
{
	public class NLogLoggerAdapter : IAppLogger
	{
		private bool _IsEnabled;
		private readonly ILogger _logger;

		public NLogLoggerAdapter(ILogger logger)
		{
			Guard.NotNull(logger, nameof(logger));
			_logger = logger;
		}

		/// <summary>
		/// Returns true if info level logs are enabled
		/// </summary>
		public bool IsEnabled => _IsEnabled;

		public void WriteInfo(string message)
		{
			if (_IsEnabled)
				_logger.Info(message);
		}

		public void WriteWarning(string message)
		{
			//always log warnings
			_logger.Warn(message);
		}

		public void WriteException(Exception exception)
		{
			//always log exceptions
			_logger.Error(exception);
		}

		/// <summary>
		/// Enables info level logs
		/// </summary>
		public void Enable()
		{
			_IsEnabled = true;
		}
	}
}
