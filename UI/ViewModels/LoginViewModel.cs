using API;
using Caliburn.Micro;
using System.Threading.Tasks;
using System.Windows.Controls;
using UI.Event;

namespace UI.ViewModels
{
    public class LoginViewModel : Screen
    {
        IEventAggregator events;
        string userName;

        public PasswordBox Password { private get; set; }


        public LoginViewModel(IEventAggregator events)
        {
            this.events = events;
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public async Task LogInAsync()
        {
            bool logedIn = await Program.Communicator.LogIn(userName, Password);

            if (logedIn)
            {
                events.PublishOnUIThread(EventsEnum.LogOn);
            }
        }
    }
}
