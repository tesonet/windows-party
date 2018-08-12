using System.Security;
using System.Windows;
using System.Windows.Controls;
using Tesonet.Client.Helpers;
using Tesonet.Client.ViewModels;

namespace Tesonet.Client.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, IHavePassword
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public SecureString Password => PasswordBox.SecurePassword;
    }
}
