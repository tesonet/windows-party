using tesonet.windowsparty.services.Navigation;
using tesonet.windowsparty.wpfapp.Navigation;
using tesonet.windowsparty.wpfapp.Views;

namespace tesonet.windowsparty.wpfapp.ViewModels
{
    public class MainWindowViewModel : BaseNotification, IMainWindowViewModel
    {
        private readonly INavigator _navigator;
        private readonly ILoginView _loginView;
        private readonly IServersView _serversView;
        private IView _currentView;

        public MainWindowViewModel(INavigator navigator, ILoginView loginView, IServersView serversView)
        {
            _loginView = loginView;
            _serversView = serversView;
            _currentView = _loginView;
            _navigator = navigator;

            _navigator.SubscribeToNavigationItem<IMainWindowViewModel, IFromLoginView>(this, OnNavigationFromLoginView);
            _navigator.SubscribeToNavigationItem<IMainWindowViewModel, IFromServersView>(this, OnNavigationFromServersView);
        }

        public IView View
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                RaisePropertyChanged(nameof(View));
            }
        }

        private void OnNavigationFromLoginView(IFromLoginView fromLoginView)
        {
            View = _serversView;
            _navigator.PublishNavigationItem(new ToServersView { Token = fromLoginView.Token });
        }

        private void OnNavigationFromServersView(IFromServersView fromServersView)
        {
            View = _loginView;
            _navigator.PublishNavigationItem(new ToLoginView());
        }
    }
}
