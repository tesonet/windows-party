namespace ServerFinder.Client.Login
{
    using System.Threading;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void LogInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                await ((LoginViewModel)this.DataContext)
                    .LogIn(Username.Text, Password.SecurePassword, CancellationToken.None);
            }
        }
    }
}
