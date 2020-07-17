using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPartyTest.Client.Base
{
    [Serializable]
    public class UserData
    {
        private string _userID = string.Empty;
        private SecureString _password = new SecureString();

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public SecureString Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
