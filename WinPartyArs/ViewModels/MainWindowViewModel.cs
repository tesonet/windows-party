using System;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using WinPartyArs.Common;
using WinPartyArs.Views;

namespace WinPartyArs.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ILoggerFacade _log;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;

        public string MainRegionName { get => "MainRegion"; }

        public MainWindowViewModel(ILoggerFacade log, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _eventAggregator.GetEvent<LoginStatusChangedEvent>().Subscribe(LoginStatusChanged, ThreadOption.UIThread);
            _regionManager.RegisterViewWithRegion(MainRegionName, typeof(Login));
            _log.Log($"MainWindowViewModel() ctor finished initializing and registered '{nameof(Login)}' within '{MainRegionName}' region", Category.Debug);
        }

        private void LoginStatusChanged(bool loggedIn)
        {
            string navigateTo = loggedIn ? nameof(ServerList) : nameof(Login);
            _log.Log($"MainWindowViewModel got loggedIn status change to '{loggedIn}', so navigating to '{navigateTo}'", Category.Info);
            _regionManager.RequestNavigate(MainRegionName, navigateTo);
        }
    }
}
