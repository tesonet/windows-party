using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.Logic;
using WindowsPartyTest.Messages;
using WindowsPartyTest.ViewModels.Interfaces;

namespace WindowsPartyTest.ViewModels
{
    public class HeaderViewModel : IHeaderViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void LogOut()
        {
            LoginLogic logic = new LoginLogic(IoC.Get<ILoginService>());
            logic.LogOut();
            _eventAggregator.PublishOnUIThread(new NavigationMessage() { Page = Enums.NavigationPages.LogIn });
        }
    }
}
