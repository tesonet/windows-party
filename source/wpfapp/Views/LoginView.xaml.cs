using System.Windows.Controls;
using tesonet.windowsparty.wpfapp.ViewModels;

namespace tesonet.windowsparty.wpfapp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, ILoginView
    {
        public LoginView(ILoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public string Password => PasswordBoxPass.Password;

        public void ClearPassword()
        {
            PasswordBoxPass.Clear();
        }
    }
}
