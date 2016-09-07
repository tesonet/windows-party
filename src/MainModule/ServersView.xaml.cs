using System.Windows.Controls;

namespace MainModule
{
    public partial class ServersView : UserControl
    {
        public ServersView(ServersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
