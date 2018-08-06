using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsParty.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
    }

    public abstract class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null, params string[] dependingPropertyNames)
        {
            bool valueChanged = !EqualityComparer<T>.Default.Equals(backingField, newValue);
            if (valueChanged)
            {
                backingField = newValue;
                RaisePropertyChanged(propertyName);

                if (dependingPropertyNames != null)
                    foreach (var name in dependingPropertyNames)
                        RaisePropertyChanged(name);
            }
            return valueChanged;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
