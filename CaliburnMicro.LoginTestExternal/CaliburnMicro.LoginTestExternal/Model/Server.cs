using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaliburnMicro.LoginTestExternal.Model
{
 public class Server
    {
        public Server(string name, string distance)
        {
            Name = name;
            Distance = distance;


        }
        public string Name { get; set;}
        public string Distance { get; set; }

    }
}
