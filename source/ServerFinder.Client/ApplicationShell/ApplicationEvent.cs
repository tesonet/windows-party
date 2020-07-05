namespace ServerFinder.Client.ApplicationShell
{
    public class ApplicationEvent
    {
        public ApplicationEvent(ApplicationEventType type)
        {
            Type = type;
        }

        public ApplicationEventType Type { get; private set; }
    }

    public enum ApplicationEventType
    {
        LogIn,
        LogOut
    }
}
