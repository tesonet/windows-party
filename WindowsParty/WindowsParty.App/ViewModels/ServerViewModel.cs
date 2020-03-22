using Caliburn.Micro;

namespace WindowsParty.App.ViewModels
{
    public class ServerViewModel
    {
        public ServerViewModel(
            string name, 
            string distance)
        {
            Name = name;
            Distance = distance;
        }

        public string Name { get; }

        public string Distance { get; }
    }
}
