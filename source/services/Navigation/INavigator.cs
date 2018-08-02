using System;

namespace tesonet.windowsparty.services.Navigation
{
    public interface INavigator
    {
        void SubscribeToNavigationItem<TSubscriber, TNavigationItem>(TSubscriber subscriber, Action<TNavigationItem> handler) where TNavigationItem : INavigationItem;

        void PublishNavigationItem<TNavigationItem>(TNavigationItem navigationItem) where TNavigationItem : INavigationItem;
    }
}
