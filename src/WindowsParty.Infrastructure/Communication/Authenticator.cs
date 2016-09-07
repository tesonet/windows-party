using System.Net;
using WindowsParty.Infrastructure.Domain;
using Newtonsoft.Json;
using RestSharp;

namespace WindowsParty.Infrastructure.Communication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IRestClient _client;

        public string Token { get; private set; }

        public Authenticator(IRestClient client)
        {
            _client = client;
            Token = "";
        }
        public HttpStatusCode Authenticate(string username, string password)
        {
            var request = new RestRequest("tokens", Method.POST);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = _client.Execute(request);

            if (response == null) return HttpStatusCode.BadRequest;

            Token = RetrieveToken(response);
            return response.StatusCode;
        }

        private static string RetrieveToken(IRestResponse response)
        {
            var responseData = JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content ?? "");
            return responseData != null ? responseData.Token : "";
        }
    }



}