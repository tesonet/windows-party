using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WinPartyArs.Abstracts;
using WinPartyArs.Common;

namespace WinPartyArs.ViewModels
{
    public class ServerListViewModel : BindableBase, INavigationAware
    {
        private ILoggerFacade _log;
        private IEventAggregator _eventAggregator;
        private TesonetServiceAbstract _tesonetService;

        private CancellationTokenSource _getServersCancelationTokenSource;

        #region Properties
        private ListCollectionView servers;
        public ListCollectionView Servers
        {
            get { return servers; }
            set { SetProperty(ref servers, value); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { SetProperty(ref errorMsg, value); }
        }

        public ICommand LogoutCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SortCommand { get; set; }
        #endregion

        public ServerListViewModel(ILoggerFacade log, IEventAggregator eventAggregator, TesonetServiceAbstract tesonetService)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _tesonetService = tesonetService ?? throw new ArgumentNullException(nameof(tesonetService));
            _eventAggregator.GetEvent<LoginStatusChangedEvent>().Subscribe(LoginStatusChanged);
            LogoutCommand = new DelegateCommand(ExecuteLogOut);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
            SortCommand = new DelegateCommand<string>(ExecuteSort);
            _log.Log("ServerListViewModel() ctor finished initializing", Category.Debug);
        }

        private void LoginStatusChanged(bool loggedIn)
        {
            if (!loggedIn)
            {
                _log.Log("User logged out, clearing server list", Category.Debug);
                Servers = null;
            }
        }

        /// <summary>
        /// To support cancelation on another call of refresh, we can use private field _getServersCancelationTokenSource.
        /// So it cancels previous request first, if there is still _getServersCancelationTokenSource, sets IsBusy = true and
        /// starts downloading servers. Afterwards it creates new ListCollectionView from results and copies SortDescriptions, if any.
        /// Finally removes _getServersCancelationTokenSource, if it's this call's one (saved in oldCancelToken var) and disposes it.
        /// </summary>
        private async void GetServers()
        {
            CancellationTokenSource oldCancelToken;
            lock (this)
            {
                oldCancelToken = _getServersCancelationTokenSource;
                _getServersCancelationTokenSource = new CancellationTokenSource();
                if (null != oldCancelToken)
                {
                    oldCancelToken.Cancel();
                    _log.Log("GetServers() called, but previous request is not done yet, so cancelling it.", Category.Debug);
                }
                oldCancelToken = _getServersCancelationTokenSource;
                IsBusy = true;
            }

            try
            {
                ErrorMsg = null;
                _log.Log($"GetServers() calling GetServerList() servers", Category.Info);
                var res = await _tesonetService.GetServerList(oldCancelToken.Token);
                _log.Log($"GetServers() got {(null != res.Count ? res.Count : "unknown count of")} servers", Category.Info);
                ErrorMsg = null;

                var newServers = new ListCollectionView(res);
                var oldSortDescriptions = Servers?.SortDescriptions;
                if (null != oldSortDescriptions && oldSortDescriptions.Count > 0)
                    for (int i = 0; i < oldSortDescriptions.Count; i++)
                        newServers.SortDescriptions.Add(oldSortDescriptions[i]);
                Servers = newServers;
            }
            catch (TaskCanceledException)
            {
                _log.Log($"GetServers() current request was canceled", Category.Debug);
            }
            catch (Exception ex)
            {
                _log.Log($"GetServers() exception: {ex.ToString()}", Category.Warn);
                ErrorMsg = $"Getting servers error: {StaticHelpers.GetInnerMostException(ex).Message}";
            }
            finally
            {
                lock (this)
                {
                    if (_getServersCancelationTokenSource == oldCancelToken)
                    {
                        IsBusy = false;
                        _getServersCancelationTokenSource = null;
                        _log.Log($"GetServers() done, nulling last token source", Category.Debug);
                    }
                    else
                        _log.Log($"GetServers() done, but last token source is already different, so leaving it as it is", Category.Debug);
                }
                oldCancelToken.Dispose();
            }
        }

        #region CommandHandlers
        private void ExecuteRefresh()
        {
            _log.Log($"ServerListViewModel Refresh command called, calling GetServers()", Category.Info);
            GetServers();
        }

        private void ExecuteLogOut()
        {
            _log.Log($"ServerListViewModel LogOut command called, calling DoLogout()", Category.Info);
            _tesonetService.DoLogout();
        }

        /// <summary>
        /// Sort by clearing and adding SortDescription to ListCollectionView. If previously sorted by the same propertyName Ascending,
        /// then sort descending this time. If we need to show sorting icons (there were none in images), we should notify with new properties.
        /// It's possible to check SortDescriptions from View by accessing 'Servers' property, but it wouldn't be too MVVM though.
        /// </summary>
        /// <param name="propertyName">Name of property to sort on</param>
        private void ExecuteSort(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var serversLocalVar = Servers;
            if (null != serversLocalVar)
            {
                var direction = ListSortDirection.Ascending;
                lock (serversLocalVar.SortDescriptions)
                {
                    if (serversLocalVar.SortDescriptions.Count == 1)
                    {
                        var oldSd = serversLocalVar.SortDescriptions[0];
                        if (oldSd.PropertyName == propertyName && oldSd.Direction == ListSortDirection.Ascending)
                            direction = ListSortDirection.Descending;
                    }

                    Servers.SortDescriptions.Clear();
                    Servers.SortDescriptions.Add(new SortDescription(propertyName, direction));
                }
            }
        }
        #endregion


        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _log.Log($"Navigating to ServerListViewModel, so calling GetServers()", Category.Info);
            GetServers();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        #endregion
    }
}
