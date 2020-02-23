namespace WindowsParty.App.Models
{
    public class ServerModel
    {

        private int _distance;

        public string Server { get; set; }

        public int Distance
        {
            get { return _distance; }
            set 
            { 
                _distance = value;
                LocalizedDistance = string.Format("{0} km", value);
            }
        }

        public string LocalizedDistance { get; set; }

    }
}
