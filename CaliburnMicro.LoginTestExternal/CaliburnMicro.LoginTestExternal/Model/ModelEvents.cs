using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaliburnMicro.LoginTestExternal.Model
{
    public class ModelEvents
    {
        public List<Server> EventList { get; private set; }
        public ModelEvents(List<Server> objects)
        {

            this.EventList = objects;
        }

    }
}
