namespace ServerList.Models
{
    public class Server
    {
        private string _name;
        private string _distance;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Distance
        {
            get { return $"{_distance} km"; }
            set { _distance = value; }
        }
    }
}
