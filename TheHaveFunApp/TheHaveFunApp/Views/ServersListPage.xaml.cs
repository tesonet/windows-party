using System.Windows.Controls;
using TheHaveFunApp.ViewModels;

namespace TheHaveFunApp.Views
{
    /// <summary>
    /// Interaction logic for ServersListPage.xaml
    /// </summary>
    public partial class ServersListPage : UserControl
    {
        public ServersListPage()
        {
            InitializeComponent();
        }

        public ServersListPage(ServersListPageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
