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
using System.Windows.Shapes;

namespace TesonetTestTask
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private ConnectionMgt con = new ConnectionMgt();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            AppContext.Token = con.GetToken("https://playground.tesonet.lt/v1/tokens", logUserName.Text, logPassword.Password);            
            if (!string.IsNullOrWhiteSpace(AppContext.Token))
            {
                ServerList sl = new ServerList();
                sl.Show();
                this.Close();
            }

            //con.GetServerList("https://playground.tesonet.lt/v1/servers");
        }
        

    }
}
