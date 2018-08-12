using GalaSoft.MvvmLight.Messaging;

namespace Tesonet.Client.Services.MessengerService
{
    public interface IMessengerService
    {
        IMessenger Default { get; }
    }
}