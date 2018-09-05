using Core;
using Core.MVVM;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Repository.Model;
using Repository.Repository.Interface;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace ServerModule.ViewModel
{
    [Export]
    [RegionMemberLifetime(KeepAlive = false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServerViewModel : NotifyPropertyChanged, INavigationAware
    {
        #region Fields
        private IServerRepository _serverRepository;
        private Authentication _authentic;
        private ServerFarm _farm;
        #endregion

        #region Properties
        public ServerFarm Farm
        {
            get { return _farm; }
            set
            {
                _farm = value;
                OnPropertyChanged(() => Farm);
            }
        }
        #endregion

        #region ctor
        [ImportingConstructor]
        public ServerViewModel(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;

            LogoutCommand = new AsyncCommand(LogoutCommandExecute, LogoutCommandCanExecute);            
        }
        #endregion

        #region Methods
        private async void GetServers()
        {
            Farm = await _serverRepository.GetServerFarmAsync(_authentic);
        }
        public void OnNavigatedFrom(Microsoft.Practices.Prism.Regions.NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add("PageFrom", this.ToString());
        }

        public void OnNavigatedTo(Microsoft.Practices.Prism.Regions.NavigationContext navigationContext)
        {
            int hash = int.Parse(navigationContext.Parameters["authenticationhash"]);
            _authentic = (Authentication)NavigatePatameters.Request(hash);
            GetServers();
        }

        public bool IsNavigationTarget(Microsoft.Practices.Prism.Regions.NavigationContext navigationContext)
        {
            return false;
        }

        #endregion

        #region Commands
        private bool LogoutCommandCanExecute(object arg) => true;

        private void LogoutCommandExecute(object obj)
        {
            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                regionManager.RequestNavigate("MainRegion", new Uri("LoginView", UriKind.Relative), nr =>
                {
                    if (nr.Result.HasValue && nr.Result == false)
                    {
                        var error = nr.Error;
                    }
                });
            }));
        }

        public ICommand LogoutCommand { get; private set; }
        #endregion
    }
}
