using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HomeWork
    {
    public interface ILogInPage
        {
        void LogIn (object sender, RoutedEventArgs e);
        void Show ();
        }

    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogInPage : Window, ILogInPage
        {
        public LogInPage ()
            {
            InitializeComponent ();
            }

        public async void LogIn (object sender, RoutedEventArgs e)
            {
            BadPassword.Visibility = Visibility.Hidden;
            BadUserName.Visibility = Visibility.Hidden;
            BadCredentials.Visibility = Visibility.Hidden;

            bool legitCredentials = true;

            string userName = UserName.Text;
            string password = Password.Text;

            if ( String.IsNullOrEmpty (userName) )
                {
                legitCredentials = false;

                BadUserName.Visibility = Visibility.Visible;
                }

            if ( String.IsNullOrEmpty (password) )
                {
                legitCredentials = false;

                BadPassword.Visibility = Visibility.Visible;
                }

            if ( !legitCredentials )
                return;


            using ( var connectionClient = new MyHttpClient (userName, password) )
                {
                try
                    {
                    await connectionClient.SetToken ();
                    }
                catch ( ArgumentException ex )
                    {
                    if ( ex.Message == "Bad credentials." )
                        BadCredentials.Visibility = Visibility.Visible;

                    return;
                    }

                List<Server> availableServers = await connectionClient.GetServers ();
                var serversWindow = DependencyFactory.Resolve<IServerListPage> ();
                serversWindow.LoadServers (availableServers);
                serversWindow.Show ();
                }

            Close ();
            }
        }
    }
