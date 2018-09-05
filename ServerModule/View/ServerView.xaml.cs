using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Regions;
using ServerModule.ViewModel;
using System.ComponentModel.Composition;

namespace ServerModule.View
{
    [Export("ServerView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class ServerView : MetroContentControl
    {
        [ImportingConstructor]
        public ServerView(ServerViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
