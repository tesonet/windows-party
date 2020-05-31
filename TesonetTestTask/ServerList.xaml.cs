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
using TesonetTestTask.ViewModel;

namespace TesonetTestTask
{
    /// <summary>
    /// Interaction logic for ServerList.xaml
    /// </summary>
    public partial class ServerList : Window
    {
        public ServerList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           ServerViewModel serverViewModelObj = new ServerViewModel();
            serverViewModelObj.LoadServers();

            ListViewServers.DataContext = serverViewModelObj;

        }
    }
}
