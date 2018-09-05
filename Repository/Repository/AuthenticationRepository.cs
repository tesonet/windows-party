using Newtonsoft.Json;
using Repository.Model;
using Repository.Repository.Interface;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    [Export(typeof(IAuthenticationRepository))]
    public class AuthenticationRepository : IAuthenticationRepository
    {
        #region Properties
        public Authentication Authentic { get; set; }
        #endregion

        #region ctor
        private AuthenticationRepository()
        {
            Authentic = new Authentication();
        }
        #endregion

        #region Methods
        public async Task PostAuthorizationAsync(User user)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(Authentic.TokenUrl, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
                await EstimateResponse(response);
            }
        }

        private async Task EstimateResponse(HttpResponseMessage response)
        {
            switch (Authentic.StatusCode = response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());
                    Authentic.IsAuthorised = true;
                    Authentic.Token = token.Token;
                    break;
                case HttpStatusCode.Unauthorized:
                    Authentic.IsAuthorised = false;
                    break;
                default:
                    Authentic.IsAuthorised = false;
                    break;
            }
        }
        #endregion
    }

    public class TokenModel
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
