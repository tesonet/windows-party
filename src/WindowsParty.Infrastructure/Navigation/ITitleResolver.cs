using System.ComponentModel;

namespace WindowsParty.Infrastructure.Navigation
{
    public interface ITitleResolver: INotifyPropertyChanged
    {
        void ChangeTitle(string title);
    }
}