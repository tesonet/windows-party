using System;
using System.Windows;
using WindowsParty.Constants;

namespace WindowsParty.Helpers
{
	public static class MessageViewer
	{
		#region Static Methods

		public static void ShowError(Exception ex)
		{
			while (ex.InnerException != null) ex = ex.InnerException;
			ShowError(ex.Message);
		}

		public static void ShowError(string message, string title = DefaultValues.ERROR_UNHANDLED_ERROR_TITLE)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		#endregion
	}
}
