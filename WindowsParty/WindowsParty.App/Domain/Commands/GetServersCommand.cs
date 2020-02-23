namespace WindowsParty.App.Domain.Commands
{
    public class GetServersCommand : ICommand
    {
        public string Token { get; }

        public GetServersCommand(string token)
        {
            Token = token;
        }
    }
}
