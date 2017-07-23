using System;
using System.Net;

namespace TesonetWpfApp.Business
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }

        public RestException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
