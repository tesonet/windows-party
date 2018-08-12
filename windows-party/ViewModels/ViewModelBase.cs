using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tesonet.Windows.Party.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName]string propertyName = null)
        {
            if (Equals(member, val)) return;

            member = val;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
