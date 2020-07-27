namespace ServerList.Messages
{
    public class NavigationMessage
    {
        public PageName pageName;

        public NavigationMessage(PageName pageName)
        {
            this.pageName = pageName;
        }
    }

    public enum PageName
    {
        Login,
        ServerList
    }
}
