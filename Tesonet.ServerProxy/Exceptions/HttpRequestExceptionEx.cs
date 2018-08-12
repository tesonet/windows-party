using System;
using System.Net;

namespace Tesonet.ServerProxy.Exceptions
{
    public class HttpRequestExceptionEx : Exception
    {
        public HttpRequestExceptionEx()
        {
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public HttpStatusCode StatusCode { get; }
        public string Content { get; }
    }
}