using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testio.HandleMessages.Navigation
{
    public class NavigationMessage
    {
        public NavigationMessage(TargetWindow targetWindow)
        {
            TargetWindow = targetWindow;
        }

        public TargetWindow TargetWindow
        {
            get;
            private set;
        }        
    }
}
