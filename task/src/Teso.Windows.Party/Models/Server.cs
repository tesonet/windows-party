using System;

namespace Teso.Windows.Party.Models
{
    public class Server
    {
        public string Name { set; get; }

        private string _distance;
        public string Distance
        {
            set => _distance = value;
            get => $"{_distance} km";
        }
    }
}