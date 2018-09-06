using NLog.Fluent;
using System;
using System.Windows;
using System.Windows.Controls;
using Tesonet.Views;

namespace Tesonet
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var token = User.GetToken(Username.Text, Password.Password);
                if (!String.IsNullOrWhiteSpace(token))
                    DataContext = new Servers(token);
                else throw new UserAuthenticationException("Token authentication exception", "Error occurred");
            }
            catch (UserAuthenticationException exception)
            {
                Log.Error().Exception(exception).Message(exception.Message).Write();
                Label_Login.Content = exception.UiMessage;
            }
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
    }
}
