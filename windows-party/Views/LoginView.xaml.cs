using Autofac;
using System.Windows;
using System.Windows.Controls;
using Tesonet.Windows.Party.ViewModels;

namespace Tesonet.Windows.Party.Views
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LogInView : UserControl
    {
        public LogInView()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwBox = (PasswordBox)sender;
            if (DataContext != null)
                ((dynamic)DataContext).Password = pwBox.Password;

            if (pwBox.Password.Length == 0)
                PasswordHint.Visibility = Visibility.Visible;
            else
                PasswordHint.Visibility = Visibility.Collapsed;
        }
    }
}
