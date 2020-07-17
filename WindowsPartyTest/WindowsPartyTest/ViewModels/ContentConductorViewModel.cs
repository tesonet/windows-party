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
    public class ContentConductorViewModel : Conductor<object>.Collection.OneActive, IContentConductorViewModel,
        IHandle<NavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMainViewModel _mainViewModel;
        private readonly IHeaderViewModel _headerViewModel;

        public ContentConductorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _mainViewModel = IoC.Get<IMainViewModel>();
            _headerViewModel = IoC.Get<IHeaderViewModel>();

            Items.Add(_mainViewModel);
        }

        public IHeaderViewModel Header 
        { 
            get { return _headerViewModel; } 
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_mainViewModel);
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(NavigationMessage message)
        {
            //Implementation for later possible windows
        }
    }
}
