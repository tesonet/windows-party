using WindowsParty.Utils;
using WindowsParty.ViewModels;

namespace WindowsParty.Views
{
    [ViewFor(typeof(IServerListViewModel))]
    public partial class ServerListView 
    {
        public ServerListView()
        {
            InitializeComponent();
        }
    }
}
