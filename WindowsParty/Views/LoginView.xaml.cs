using System.Security;
using System.Windows.Controls;
using WindowsParty.Utils;

namespace WindowsParty.Views
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

        public SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }
    }
}
