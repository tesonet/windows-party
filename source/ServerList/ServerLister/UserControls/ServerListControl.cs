using System;
using System.Windows.Forms;
using ServerListerApp.Interfaces;
using Unity;
using UContainer = ServiceLister.Common.Implementation.UnityConfig;

namespace ServerListerApp.UserControls
{
    public partial class ServerListControl : UserControl
    {
        private IServerListController _serverListController;

        public ServerListControl()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _serverListController.LogOut();
        }

        private void ServerListControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _serverListController = UContainer.Instance.Container.Resolve<IServerListController>();
                serverListGridControl.DataSource = _serverListController.ServerList;
            }
        }
    }
}