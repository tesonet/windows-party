using System.Windows.Controls;

namespace MainModule
{
    public partial class LoginView : UserControl
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
