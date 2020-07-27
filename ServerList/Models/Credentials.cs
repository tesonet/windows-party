namespace ServerList.Models
{
    public class Credentials
    {
        public Credentials(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string username;
        public string password;
    }
}
