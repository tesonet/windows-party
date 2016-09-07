using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsParty.Infrastructure.Navigation
{
    public class TitleResolver : ITitleResolver
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentTitle { get; private set; }

        public TitleResolver()
        {
            CurrentTitle = AppViews.InitialView;
        }

        public void ChangeTitle(string title)
        {
            CurrentTitle = title;
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(CurrentTitle));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}