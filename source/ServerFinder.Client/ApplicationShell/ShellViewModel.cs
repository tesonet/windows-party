namespace ServerFinder.Client.ApplicationShell
{
    using Caliburn.Micro;
    using Login;
    using ServersList;

    public class ShellViewModel : Conductor<Screen>, IHandle<ApplicationEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(ApplicationEvent message)
        {            
            switch(message.Type)
            {
                case ApplicationEventType.LogIn:
                    ActivateItem(IoC.Get<ServersListViewModel>());
                    break;
                case ApplicationEventType.LogOut:
                    ActivateItem(IoC.Get<LoginViewModel>());
                    break;
            }
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();
            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}
