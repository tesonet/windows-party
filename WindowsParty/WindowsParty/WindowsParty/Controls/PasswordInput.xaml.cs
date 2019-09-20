using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
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
		#region Constructors

		public PasswordInput()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("PasswordValue", typeof(string), typeof(PasswordInput)
			, new PropertyMetadata(default(string)));

		[Bindable(true)]
		public string PasswordValue
		{
			get
			{
				return (string)this.GetValue(PasswordProperty);
			}
			set
			{
				this.SetValue(PasswordProperty, value);
				if (PasswordText.Password != value)
					PasswordText.Password = value;
			}
		}

		#endregion

		#region Methods

		private void OnPasswordChanged(object sender, RoutedEventArgs e)
		{
			if (PasswordText.Password.Length == 0)
			{
				PasswordValue = default(string);
				Placeholder.Visibility = Visibility.Visible;
			}
			else
			{
				PasswordValue = PasswordText.Password;
				Placeholder.Visibility = Visibility.Collapsed;
			}
		}

	#endregion
}
}
