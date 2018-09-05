using System.Net;

namespace Repository.Model
{
    public class Authentication
    {
        #region Properties
        public readonly string TokenUrl = "http://playground.tesonet.lt/v1/tokens";

        public readonly string ServersUrl = "http://playground.tesonet.lt/v1/servers";

        public bool IsAuthorised { get; set; }
        public string Token { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        #endregion
    }
}
