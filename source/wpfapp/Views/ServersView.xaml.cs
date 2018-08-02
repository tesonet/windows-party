using System.Windows.Controls;
using tesonet.windowsparty.wpfapp.ViewModels;

namespace tesonet.windowsparty.wpfapp.Views
{
    /// <summary>
    /// Interaction logic for ServersView.xaml
    /// </summary>
    public partial class ServersView : UserControl, IServersView
    {
        public ServersView(IServersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
