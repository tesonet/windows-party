using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// Interaction logic for UsernameInput.xaml
	/// </summary>
	public partial class UsernameInput : UserControl
	{
		#region Constructors

		public UsernameInput()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(UsernameInput));

		[Bindable(true)]
		public string Username
		{
			get
			{
				return (string)this.GetValue(UsernameProperty);
			}
			set
			{
				this.SetValue(UsernameProperty, value);
			}
		}

		#endregion
	}
}
