using GalaSoft.MvvmLight.Messaging;

namespace Tesonet.Client.Services.MessengerService
{
    public class MessengerService : IMessengerService
    {
        public IMessenger Default => Messenger.Default;
    }
}