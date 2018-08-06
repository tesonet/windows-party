using System.Windows;
using WindowsParty.Utils;
using WindowsParty.ViewModels;

namespace WindowsParty.Views
{
    [ViewFor(typeof(ILoginViewModel))]
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ILoginViewModel;
            if (vm == null)
                return;

            if (vm.AuthorizeCommand.CanExecute(txtPassword.Password))
                vm.AuthorizeCommand.Execute(txtPassword.Password);
        }
    }

}
