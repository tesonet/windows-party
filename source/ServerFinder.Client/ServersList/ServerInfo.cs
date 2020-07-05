namespace ServerFinder.Client.ServersList
{
    public class ServerInfo
    {
        public ServerInfo(string title, string distance)
        {
            Title = title;
            Distance = distance;
        }

        public string Title { get; set; }

        public string Distance { get; set; }
    }
}
