using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Messages;
using WindowsPartyTest.ViewModels.Interfaces;

namespace WindowsPartyTest.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IShellViewModel,
        IHandle<NavigationMessage>, IHandle<SuccessfulLoginMessage>
    {
        private IEventAggregator _eventAggregator;
        private ILoginConductorViewModel _loginConViewModel;
        private IContentConductorViewModel _contentConViewModel;
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _loginConViewModel = IoC.Get<ILoginConductorViewModel>();
            _contentConViewModel = IoC.Get<IContentConductorViewModel>();

            Items.AddRange(new object[] { _loginConViewModel, _contentConViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginConViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(NavigationMessage message)
        {
            if (message.Page == Enums.NavigationPages.LogIn)
            {
                _loginConViewModel.ClearLogin();
                ActivateItem(_loginConViewModel);
            }
        }

        public void Handle(SuccessfulLoginMessage message)
        {
            ActivateItem(_contentConViewModel);
        }
    }
}