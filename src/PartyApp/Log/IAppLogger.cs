using System;

namespace PartyApp.Log
{
	public interface IAppLogger
	{
		void WriteInfo(string message);

		void WriteWarning(string message);

		void WriteException(Exception exception);

		void Enable();

		bool IsEnabled { get; }
	}
}
