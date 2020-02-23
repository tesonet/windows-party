
namespace WindowsParty.App.Domain.Events
{
    public class LoginFailedEvent
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
