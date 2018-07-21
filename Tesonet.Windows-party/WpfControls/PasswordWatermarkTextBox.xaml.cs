using System.Windows;
using System.Windows.Controls;
using Tesonet.Windows_party.Infrastructure;

namespace Tesonet.Windows_party.WpfControls
{
    /// <summary>
    /// Interaction logic for PasswordTextBox.xaml
    /// </summary>
    public partial class PasswordWatermarkTextBox : UserControl
    {
        public PasswordWatermarkTextBox()
        {
            InitializeComponent();
        }

        private void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            watermark.Visibility = passwordBox.Password.IsEmpty() ? Visibility.Visible : Visibility.Collapsed;
        }

        public PasswordBox PasswordBox => passwordBox;
    }
}
