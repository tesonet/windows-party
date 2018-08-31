using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ServerListerApp.Controller;
using ServerListerApp.Interfaces;
using ServerListerApp.UserControls;
using Unity;
using UContainer = ServiceLister.Common.Implementation.UnityConfig;

namespace ServerListerApp
{
    public partial class MainForm : XtraForm
    {
        private UserControl _currentUserControl;

        private IMainFormController _mainFormController;

        public MainForm()
        {
            InitializeComponent();
            LoadControl(new LoginPageControl());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _mainFormController = UContainer.Instance.Container.Resolve<MainFormController>();
                _mainFormController.PropertyChanged += _mainFormController_PropertyChanged;
                HandleDestroyed += MainForm_HandleDestroyed;
            }
        }

        private void MainForm_HandleDestroyed(object sender, EventArgs e)
        {
            _mainFormController.PropertyChanged -= _mainFormController_PropertyChanged;
            _mainFormController.Dispose();
        }

        private void _mainFormController_PropertyChanged(object sender,
            PropertyChangedEventArgs e)
        {
            switch (_mainFormController.ActiveUserControl)
            {
                case ActiveUserControl.Login:
                    LoadControl(new LoginPageControl());
                    break;
                case ActiveUserControl.ServerList:
                    LoadControl(new ServerListControl());
                    break;
            }
        }

        private void LoadControl(UserControl control)
        {
            mainPC.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            control.Visible = true;

            if (_currentUserControl != null)
            {
                _currentUserControl.Visible = false;
                mainPC.Controls.Remove(_currentUserControl);
                _currentUserControl.Dispose();
            }

            _currentUserControl = control;
        }
    }
}