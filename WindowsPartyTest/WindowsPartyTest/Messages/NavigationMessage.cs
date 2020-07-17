using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPartyTest.Enums;
using WindowsPartyTest.ViewModels.Interfaces;

namespace WindowsPartyTest.Messages
{
    public class NavigationMessage
    {
        private NavigationPages _page = NavigationPages.None;

        public NavigationPages Page 
        {
            get { return _page; }
            set { _page = value; }
        }
    }
}
