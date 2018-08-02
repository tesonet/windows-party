using PubSub;
using System;

namespace tesonet.windowsparty.services.Navigation
{
    public class Navigator : INavigator
    {
        private readonly Hub _hub;

        public Navigator()
        {
            _hub = new Hub();
        }

        public void PublishNavigationItem<TNavigationItem>(TNavigationItem navigationItem) where TNavigationItem : INavigationItem
        {
            _hub.Publish(navigationItem);
        }

        public void SubscribeToNavigationItem<TSubscriber, TNavigationItem>(TSubscriber subscriber, Action<TNavigationItem> handler) where TNavigationItem : INavigationItem
        {
            _hub.Subscribe(subscriber, handler);
        }
    }
}
