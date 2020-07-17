using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.ViewModels.Interfaces;

namespace WindowsPartyTest.ViewModels
{
    public class LoginConductorViewModel : Conductor<object>.Collection.OneActive, ILoginConductorViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoginViewModel _loginViewModel;

        public LoginConductorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _loginViewModel = IoC.Get<ILoginViewModel>();

            Items.Add(_loginViewModel);
        }

        public void ClearLogin()
        {
            _loginViewModel.ClearData();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginViewModel);
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }
    }
}
