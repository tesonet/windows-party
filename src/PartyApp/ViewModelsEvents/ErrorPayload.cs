using System;
using PartyApp.Utilities;

namespace PartyApp.ViewModelsEvents
{
	public sealed class ErrorPayload : Payload
	{
		private string _errorMessage;

		public ErrorPayload(string errorMessage)
		{
			_errorMessage = errorMessage ?? "";
		}

		public ErrorPayload(Exception exception)
		{
			Guard.NotNull(exception, nameof(exception));
			Exception = exception;
		}

		public Exception Exception { get; }

		public string GetErrorMessage()
		{
			if (FromException)
				return Exception.ToString();

			return _errorMessage;
		}

		public bool FromException { get; }
	}
}
