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

namespace WindowsPartyApp.Login.Controls
{
    /// <summary>
    /// Interaction logic for PasswordTextBox.xaml
    /// </summary>
    public partial class PasswordTextBox : UserControl
    {
        public PasswordTextBox()
        {
            InitializeComponent();
        }

        private void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            //watermark.Visibility = passwordBox.Password.IsEmpty() ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
