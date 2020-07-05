namespace ServerFinder.Client.ServersList
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ServersListView.xaml
    /// </summary>
    public partial class ServersListView : UserControl
    {
        public ServersListView()
        {
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((ServersListViewModel)this.DataContext).LogOut();
            }
        }
    }
}
