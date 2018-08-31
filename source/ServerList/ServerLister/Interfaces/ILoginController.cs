using System;
using System.ComponentModel;
using ServiceLister.Common.Interfaces;

namespace ServerListerApp.Interfaces
{
    public interface ILoginController : INotifyPropertyChanged, IDisposable
    {
        ConnectionStatus ConnectionStatus { get; set; }

        void Login(string userName, string password);
        new event PropertyChangedEventHandler PropertyChanged;
    }
}