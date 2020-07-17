using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPartyTest.Models
{
    public class ServerModel
    {
        private string _name = string.Empty;
        private int _distance = 0;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
    }
}
