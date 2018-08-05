using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsParty.ApiServices;
using WindowsParty.ApiServices.Models;
using WindowsParty.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace WindowsParty.ViewModels
{
    public interface IServerListViewModel : IViewModel
    {
        IEnumerable<Server> Servers { get; }

        ICommand LogoutCommand { get; }

    }

    public class ServerListViewModel : ViewModel, IServerListViewModel
    {
        private readonly IPlaygroundService _service;
        private IEnumerable<Server> _servers;

        public string Token { get; }

        public IEnumerable<Server> Servers
        {
            get { return _servers; }
            private set { Set(ref _servers, value); }
        }

        public ICommand LogoutCommand { get; }


        public ServerListViewModel(IPlaygroundService service, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(token);
            _service = service;

            Token = token;

            LogoutCommand = new RelayCommand(LogOut);

            LoadAsync();
        }

        private void LogOut()
        {
            Messenger.Default.Send(new AuthorizationMessage(null));
        }

        private void LoadAsync()
        {
            var task = _service.Servers(Token);
            task.ContinueWith(t => { Servers = t.Result; }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        /// <inheritdoc />
    }
}