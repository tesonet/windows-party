using System.Windows.Controls;
using TheHaveFunApp.ViewModels;

namespace TheHaveFunApp.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(LoginPageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        
        private void BrdPassword_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtPassword.Visibility = System.Windows.Visibility.Collapsed;
            entryPassword.Visibility = System.Windows.Visibility.Visible;
            entryPassword.Focus();
        }

        private void BrdUserName_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtUserName.Visibility = System.Windows.Visibility.Collapsed;
            entryUserName.Visibility = System.Windows.Visibility.Visible;            
            entryUserName.Focus();
        }

        private void EntryPassword_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(entryPassword.Password))
            {
                txtPassword.Visibility = System.Windows.Visibility.Visible;
                entryPassword.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void EntryUserName_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(entryUserName.Text))
            {
                txtUserName.Visibility = System.Windows.Visibility.Visible;
                entryUserName.Visibility = System.Windows.Visibility.Collapsed;
            }
        }     
    }
}
