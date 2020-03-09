namespace Teso.Windows.Party.Events
{
    public class ChangeEvent
    {
        public ChangeAction ChangeAction { get; }
        public object Data { get; }

        public ChangeEvent(ChangeAction action, object data = null)
        {
            ChangeAction = action;
            Data = data;
        }
    }

    public enum ChangeAction
    {
        LoggedIn,
        LoggedOut
    }
}
