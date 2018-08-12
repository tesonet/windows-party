using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using NLog;
using Tesonet.Client.Messages;
using Tesonet.Client.Services.NavigationService;
using Tesonet.Client.Services.NavigationService.NavigationData;

namespace Tesonet.Client.ViewModels.Base
{
    public abstract class NavigableViewModel : ViewModelBase
    {
        protected readonly INavigationService NavigationService;
        protected ILogger Log { get; private set; }

        private bool _isBusy;
        private string _busyMessage;

        protected NavigableViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Log = LogManager.GetLogger(GetType().FullName);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();

                Messenger.Default.Send(new IsBusyChangedMessage
                {
                    IsBusy = value,
                    Source = this
                });
            }
        }

        public string BusyMessage
        {
            get => _busyMessage;
            set
            {
                _busyMessage = value;
                RaisePropertyChanged();
            }
        }

        public virtual Task InitializeAsync(NavigationData navigationData)
        {
            return Task.FromResult(false);
        }

        public async Task ExecuteBusyActionAsync(Func<Task> action, string busyMessage)
        {
            IsBusy = true;
            BusyMessage = busyMessage;

            try
            {
                await action();
            }
            finally
            {
                BusyMessage = string.Empty;
                IsBusy = false;
            }
        }
    }
}