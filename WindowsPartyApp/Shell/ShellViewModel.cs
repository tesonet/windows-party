using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsPartyApp.Api;
using WindowsPartyApp.Login;
using WindowsPartyApp.Model;
using WindowsPartyApp.Servers;

namespace WindowsPartyApp.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<MessageItem>
    {
        private readonly LoginViewModel loginViewModel;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(LoginViewModel loginViewModel, IEventAggregator eventAggregator)
        {
            this.loginViewModel = loginViewModel;
            _eventAggregator = eventAggregator;
        }

        public async Task HandleAsync(MessageItem message, CancellationToken cancellationToken)
        {
            ActivateItem(message.ViewModel ?? loginViewModel);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _eventAggregator.SubscribeOnPublishedThread(this);
            ActivateItem(loginViewModel);
        }
    }
}
