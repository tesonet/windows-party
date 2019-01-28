using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using testio.HandleMessages.Navigation;

namespace testio.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<NavigationMessage>
    {
        #region Fields

        private IEventAggregator _eventAggregator = null;

        #endregion Fields

        #region Constructors

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            Handle(new NavigationMessage(TargetWindow.Login));
        }

        #endregion Constructors

        public void Handle(NavigationMessage navigationMessage)
        {
            switch (navigationMessage.TargetWindow)
            {
                case TargetWindow.Login:
                    ActivateItem(IoC.Get<Login.LoginViewModel>());
                    break;

                case TargetWindow.ServerList:
                    ActivateItem(IoC.Get<ServerList.ServerListViewModel>());
                    break;
            }
        }
    }
}
