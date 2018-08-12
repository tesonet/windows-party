using System;
using System.Collections.Generic;
using System.Data;
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

namespace HomeWork
    {
    public interface IServerListPage
        {
        void LoadServers (List<Server> servers);
        void Logout (object sender, RoutedEventArgs e);
        void Show ();
        }

    /// <summary>
    /// Interaction logic for ServersList.xaml
    /// </summary>
    public partial class ServersListPage : Window, IServerListPage
        {
        public ServersListPage ()
            {
            InitializeComponent ();
            }

        public void LoadServers (List<Server> servers)
            {
            DataTable serversTable = new DataTable ();

            serversTable.Columns.Add (new DataColumn ("Server", typeof (string)));
            serversTable.Columns.Add (new DataColumn ("Distance", typeof (int)));

            foreach ( Server current in servers )
                {
                DataRow currentRow = serversTable.NewRow ();
                currentRow[0] = current.Name;
                currentRow[1] = current.Distance;

                serversTable.Rows.Add (currentRow);
                }

            serversGrid.ItemsSource = serversTable.DefaultView;
            }

        public void Logout (object sender, RoutedEventArgs e)
            {
            var loginPage = DependencyFactory.Resolve<ILogInPage> ();

            loginPage.Show();
            Close ();
            }
}
    }
