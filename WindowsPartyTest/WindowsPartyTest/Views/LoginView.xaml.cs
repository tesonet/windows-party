using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
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
using WindowsPartyTest.ViewModels;
using WindowsPartyTest.Views.Interfaces;

namespace WindowsPartyTest.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, ILoginHandler
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ILoginViewModel))
                return;
            ILoginViewModel viewModel = DataContext as ILoginViewModel;
            if (viewModel == null)
                return;
            if (viewModel.Validate())
            {
                viewModel.Login();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((ILoginViewModel)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
        public void ClearPassword()
        {
            //Password box should be empty after this but it is not (Mystery problem)
            passwordBox.Clear();
            UpdateLayout();
            InvalidateVisual();
        }
    }
}
