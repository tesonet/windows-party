using System.Windows.Controls;

namespace WindowsParty.Views
{
	/// <summary>
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl
	{
		#region Constructors

		public LoginView()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void ClearPassword()
		{
			PasswordBox.PasswordValue = string.Empty;
		}

		#endregion
	}
}
