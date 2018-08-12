namespace WindowsParty.Logging
{
	public interface ILogger
	{
		void Debug(string message, params object[] args);

		void Info(string message, params object[] args);

		void Warning(string message, params object[] args);

		void Error(string message, params object[] args);

		void Trace(string message, params object[] args);
	}
}
