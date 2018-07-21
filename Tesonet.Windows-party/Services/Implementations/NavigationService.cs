using Tesonet.Windows_party.Services.Interfaces;
using Tesonet.Windows_party.Views;

namespace Tesonet.Windows_party.Services.Implementations
{
    public class NavigationService : INavigationService
    {
        private LoginView _loginView;
        private ServerListView _serverListView;

        public void ShowLoginView()
        {
            if (_loginView == null)
            {
                _loginView = new LoginView();
                _loginView.Show();
                CloseServerListView();
            }
        }

        public void ShowServerListView()
        {
            if (_serverListView == null)
            {
                _serverListView = new ServerListView();
                _serverListView.Show();
                CloseLoginView();                
            }
        }

        private void CloseLoginView()
        {
            if (_loginView != null)
            {
                _loginView.Close();
                _loginView = null;
            }
        }

        private void CloseServerListView()
        {
            if (_serverListView != null)
            {
                _serverListView.Close();
                _serverListView = null;
            }
        }
    }
}