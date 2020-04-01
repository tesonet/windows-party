using Caliburn.Micro;
using UI.Event;

namespace UI.ViewModels
{
    public class MainViewModel : Conductor<object>, IHandle<EventsEnum>
    {
        ServersViewModel serversViewModel;
        readonly SimpleContainer container;
        IEventAggregator events;

        public MainViewModel(ServersViewModel serversViewModel, IEventAggregator events, SimpleContainer container)
        {
            this.events = events;
            this.serversViewModel = serversViewModel;
            this.container = container;

            this.events.Subscribe(this);

            ActivateItem(this.container.GetInstance<LoginViewModel>());
        }

        public void Handle(EventsEnum message)
        {
            switch (message)
            {
                case EventsEnum.LogOn:
                    ActivateItem(serversViewModel);
                    break;
                case EventsEnum.LogOut:
                    ActivateItem(this.container.GetInstance<LoginViewModel>());
                    break;
            }
        }
    }
}
