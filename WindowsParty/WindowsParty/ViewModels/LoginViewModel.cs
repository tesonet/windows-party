using System.Threading.Tasks;
using System.Windows.Input;
using WindowsParty.ApiServices;
using WindowsParty.ApiServices.Models;
using WindowsParty.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace WindowsParty.ViewModels
{
    public interface ILoginViewModel : IViewModel
    {
        ICommand AuthorizeCommand { get; }

        string Token { get; }
        string Username { get; set; } 
    }


    public class LoginViewModel : ViewModel, ILoginViewModel
    {
        private readonly IPlaygroundService _service;
        private string _username = "tesonet";
        private string _token;

        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        public string Token
        {
            get { return _token; }
            private set { Set(ref _token, value); }
        }


        public LoginViewModel(IPlaygroundService service)
        {
            _service = service;
            AuthorizeCommand = new RelayCommand<string>(Authorize);
        }


        public ICommand AuthorizeCommand { get; }

        private void Authorize(string password)
        {
            var r = new AuthRequest()
            {
                Username = Username,
                Password = password
            };
            var task = _service.Authorize(r);
            task.ContinueWith(t =>
            {
                var res = t.Result;
                if (!string.IsNullOrWhiteSpace(res.Token))
                {
                    Messenger.Default.Send(new AuthorizationMessage(res.Token));
                }

                if (!string.IsNullOrWhiteSpace(res.Message))
                {
                    //TODO: Show message.
                }
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            //TODO: Authorize task fails
        }
    }
}
