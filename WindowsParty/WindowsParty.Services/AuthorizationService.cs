using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Common.Interfaces;
using WindowsParty.Common.Models;

namespace WindowsParty.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAppSettings _appSettings;
        public AuthorizationService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task<AuthorizationResultModel> GenerateToken(TokenRequestModel req)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(_appSettings.TokenUrl,
                    new StringContent(JsonConvert.SerializeObject(req), 
                    Encoding.UTF8, "application/json"));

                return await ResponseHandler(response);
            }
        }

        private async Task<AuthorizationResultModel> ResponseHandler(HttpResponseMessage response)
        {
            switch(response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<TokenResponseModel>(result);

                    return new AuthorizationResultModel
                    {
                        Success = true,
                        Token = token.Token
                    };

                case System.Net.HttpStatusCode.Unauthorized:
                    return new AuthorizationResultModel
                    {
                        Success = false,
                        Message = "Unauthorized"
                    };
                default:
                    return new AuthorizationResultModel
                    {
                        Success = false,
                        Message = "Failed to authorize"
                    };
            }
        }
    }
}
