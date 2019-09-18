using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsParty.Controls
{
	/// <summary>
	/// Interaction logic for PasswordInput.xaml
	/// </summary>
	public partial class PasswordInput : UserControl
	{
		public PasswordInput()
		{
			InitializeComponent();
		}

		private void OnPasswordChanged(object sender, RoutedEventArgs e)
		{
			if (PasswordText.Password.Length == 0)
				Placeholder.Visibility = Visibility.Visible;
			else
				Placeholder.Visibility = Visibility.Collapsed;
		}
	}
}
