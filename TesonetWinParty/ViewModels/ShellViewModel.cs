using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesonetWinParty.EventModels;

namespace TesonetWinParty.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>, IHandle<LogOutEvent>
    {
        IEventAggregator _events;
        ServersViewModel _serversViewModel;

        public ShellViewModel(IEventAggregator events, ServersViewModel serversViewModel)
        {
            _serversViewModel = serversViewModel;

            _events = events;
            _events.Subscribe(this);

            //Main app visible screen
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEventModel message)
        {
            ActivateItem(_serversViewModel);
        }

        public void Handle(LogOutEvent message)
        {
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}
