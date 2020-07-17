using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPartyTest.Client.Base
{
    public class APIConfig
    {
        private string _connectionURL = string.Empty;
        private string _serverEndPoint = string.Empty;
        private string _tokensEndPoint = string.Empty;

        public string ConnectionUrl 
        {
            get
            {
                return _connectionURL;
            }
            set
            {
                _connectionURL = value;
            }
        }
        public string ServerEndPoint
        {
            get
            {
                return _serverEndPoint;
            }
            set
            {
                _serverEndPoint = value;
            }
        }
        public string TokensEndPoint
        {
            get
            {
                return _tokensEndPoint;
            }
            set
            {
                _tokensEndPoint = value;
            }
        }
    }
}
