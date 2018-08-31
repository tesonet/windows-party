using System.ComponentModel;
using System.Runtime.CompilerServices;
using ServerListerApp.Annotations;
using ServerListerApp.Interfaces;
using ServiceLister.Common.Implementation;
using ServiceLister.Common.Interfaces;

namespace ServerListerApp.Controller
{
    internal class MainFormController : IMainFormController
    {
        private ActiveUserControl _activeUserControl;

        public MainFormController()
        {
            Authorization.Instance.PropertyChanged += Authorization_PropertyChanged;
            ActiveUserControl = ActiveUserControl.Login;
        }

        public ActiveUserControl ActiveUserControl
        {
            get => _activeUserControl;
            set
            {
                _activeUserControl = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            Authorization.Instance.PropertyChanged -= Authorization_PropertyChanged;
        }

        private void CalculateViewState()
        {
            switch (ActiveUserControl)
            {
                case ActiveUserControl.Login:
                    if (Authorization.Instance.ConnectionStatus == ConnectionStatus.Connected)
                        ActiveUserControl = ActiveUserControl.ServerList;
                    break;
                case ActiveUserControl.ServerList:
                    if (Authorization.Instance.ConnectionStatus == ConnectionStatus.Disconnected ||
                        Authorization.Instance.ConnectionStatus == ConnectionStatus.Faulted ||
                        Authorization.Instance.ConnectionStatus == ConnectionStatus.LoggedOut)
                        ActiveUserControl = ActiveUserControl.Login;
                    break;
            }
        }

        private void Authorization_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionStatus")
                CalculateViewState();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}