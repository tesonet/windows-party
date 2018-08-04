using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Tesonet.Party.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
    }

    public abstract class ViewModelBase : IViewModel
    {
        protected readonly IUnityContainer container;

        public ViewModelBase()
        {
        }

        public ViewModelBase(IUnityContainer container)
        {
            this.container = container;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected PropertyChangedEventHandler PropertyChangedHandler
        {
            get { return PropertyChanged; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null) throw new ArgumentNullException("propertyNames");
            foreach (var name in propertyNames)
            {
                this.OnPropertyChanged(name);
            }
        }
        #endregion
    }
}
