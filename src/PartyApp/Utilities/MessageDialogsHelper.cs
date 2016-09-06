using System.Windows;

namespace PartyApp.Utilities
{
	public static class MessageDialogsHelper
	{
		public static void ShowError(string message, params object[] args)
		{
			Guard.NotEmpty(message, nameof(message));
			MessageBox.Show(
				string.Format(message, args),
				Properties.Resources.ApplicationName,
				MessageBoxButton.OK,
				MessageBoxImage.Error);
		}
	}
}

