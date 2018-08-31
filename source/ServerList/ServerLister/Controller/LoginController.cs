using System.ComponentModel;
using System.Runtime.CompilerServices;
using ServerListerApp.Annotations;
using ServerListerApp.Interfaces;
using ServiceLister.Common.Implementation;
using ServiceLister.Common.Interfaces;

namespace ServerListerApp.Controller
{
    public class LoginController : ILoginController
    {
        private ConnectionStatus _connectionStatus;

        public LoginController()
        {
            Authorization.Instance.PropertyChanged += Authorization_PropertyChanged;
        }

        public ConnectionStatus ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged();
            }
        }

        public void Login(string userName, string password)
        {
            Authorization.Instance.GenerateToken(userName, password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            Authorization.Instance.PropertyChanged -= Authorization_PropertyChanged;
        }

        private void Authorization_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionStatus")
                ConnectionStatus = Authorization.Instance.ConnectionStatus;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}