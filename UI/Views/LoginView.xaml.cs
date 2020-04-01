using System.Windows;
using System.Windows.Controls;

namespace UI.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }


        private void PasswordBoxChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                PasswordBox passBox = (PasswordBox)sender;
                ((dynamic)this.DataContext).Password = passBox;
                if (passBox.SecurePassword.Length > 0)
                {
                    PasswordLabel.Visibility = Visibility.Hidden;
                }
                else
                {
                    PasswordLabel.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
