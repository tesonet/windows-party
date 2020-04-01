namespace UI.Models
{
    public class ServerInfo
    {
        public string Name { get; set; }
        public string Distance { get; set; }

        public ServerInfo() { }

        public ServerInfo(string name, string distance)
        {
            this.Name = name;
            this.Distance = distance;
        }
    }
}
