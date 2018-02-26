using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
    {
    public class Server
        {
        public string Name
            {
            get; private set;
            }

        public string Distance
            {
            get; private set;
            }

        public Server (string name, string distance)
            {
            Name = name;
            Distance = distance;
            }
        }
    }
