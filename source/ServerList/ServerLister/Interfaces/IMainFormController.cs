using System;
using System.ComponentModel;

namespace ServerListerApp.Interfaces
{
    internal interface IMainFormController : INotifyPropertyChanged, IDisposable
    {
        ActiveUserControl ActiveUserControl { get; set; }
        new event PropertyChangedEventHandler PropertyChanged;
    }
}