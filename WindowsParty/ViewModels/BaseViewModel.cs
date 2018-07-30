
using System.ComponentModel;
using Unity;

namespace WindowsParty.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IUnityContainer Container;

        public BaseViewModel(IUnityContainer container)
        {
            Container = container;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
