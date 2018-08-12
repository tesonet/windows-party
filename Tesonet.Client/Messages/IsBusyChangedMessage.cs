using Tesonet.Client.ViewModels.Base;

namespace Tesonet.Client.Messages
{
    public class IsBusyChangedMessage
    {
        public bool IsBusy { get; set; }
        public NavigableViewModel Source { get; set; }
    }
}