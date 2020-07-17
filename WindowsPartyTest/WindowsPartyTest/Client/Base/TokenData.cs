using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPartyTest.Client.Base
{
    [Serializable]
    public class TokenData
    {
        private string _token = string.Empty;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
