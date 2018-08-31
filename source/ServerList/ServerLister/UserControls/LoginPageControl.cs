using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ServerListerApp.Interfaces;
using ServiceLister.Common.Interfaces;
using Unity;
using UContainer = ServiceLister.Common.Implementation.UnityConfig;

namespace ServerListerApp.UserControls
{
    public partial class LoginPageControl : UserControl
    {
        private ILoginController _loginController;

        public LoginPageControl()
        {
            InitializeComponent();
        }

        private void LoginPageControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _loginController = UContainer.Instance.Container.Resolve<ILoginController>();
                _loginController.PropertyChanged += _loginController_PropertyChanged;
                HandleDestroyed += LoginPageControl_HandleDestroyed;
            }
        }

        private void LoginPageControl_HandleDestroyed(object sender, EventArgs e)
        {
            _loginController.PropertyChanged -= _loginController_PropertyChanged;
            _loginController.Dispose();
        }

        private void _loginController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionStatus")
            {
                if (_loginController.ConnectionStatus == ConnectionStatus.Disconnected)
                    XtraMessageBox.Show("Connection has expired", "Disconnected", MessageBoxButtons.OK);

                if (_loginController.ConnectionStatus == ConnectionStatus.Faulted)
                    XtraMessageBox.Show("Faild to connect", "Error", MessageBoxButtons.OK);
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            _loginController.Login(userNameTE.Text, passwordTE.Text);
        }
    }
}