using Core;
using Core.MVVM;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Repository.Model;
using Repository.Repository.Interface;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace LoginModule.ViewModel
{
    [Export]
    [RegionMemberLifetime(KeepAlive = false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginViewModel : NotifyPropertyChanged
    {
        #region Fields
        private User _user;
        private IAuthenticationRepository _authenticationRepository;
        #endregion
                
        #region ctor
        [ImportingConstructor]
        public LoginViewModel(IAuthenticationRepository authenticationRepository)
        {
            LoginCommand = new AsyncCommand(LoginCommandExecuteAsync, LoginCommandCanExecute);

            _authenticationRepository = authenticationRepository;
            _user = new User();
        }
        #endregion

        #region Properties        
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(() => User);
            }
        }
        #endregion

        #region Methods

        #endregion

        #region Commands
        private bool LoginCommandCanExecute(object arg) => System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

        private async void LoginCommandExecuteAsync(object obj)
        {
            await _authenticationRepository.PostAuthorizationAsync(User);
            if (_authenticationRepository.Authentic.IsAuthorised)
            {
                UriQuery query = new UriQuery();
                NavigatePatameters.Save(_authenticationRepository.Authentic.GetHashCode(), _authenticationRepository.Authentic);
                query.Add("authenticationhash", _authenticationRepository.Authentic.GetHashCode().ToString());
                
                Application.Current.Dispatcher.Invoke(new System.Action(() =>
                {
                    var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                    regionManager.RequestNavigate("MainRegion", new Uri("ServerView" + query.ToString(), UriKind.Relative), nr =>
                    {
                        if (nr.Result.HasValue && nr.Result == false)
                        {
                            var error = nr.Error;
                        }
                    });
                }));
            }
            else
            {
                MessageBox.Show("Unable to authenticate!", "Authorization error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand LoginCommand { get; private set; }
        #endregion
    }
}
