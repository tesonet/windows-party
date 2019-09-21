using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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

		public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("PasswordValue", typeof(string), typeof(PasswordInput), new PropertyMetadata(default(string)));

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
