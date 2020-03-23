using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.App.Configurations;
using WindowsParty.App.Data;
using WindowsParty.App.Interfaces;
using WindowsParty.App.Models.V1;

namespace WindowsParty.App.Services
{
    public class UserService : HttpDataProvider, IUserService
    {
        public UserService(HttpDataConfiguration httpDataConfiguration) 
            : base(httpDataConfiguration)
        { }

        public async Task<Bearer> GetToken(string username, string password)
        {
            var resourceName = "tokens";
            var user = new 
            {
                username,
                password
            };

            var content = new StringContent(Serialize(user), Encoding.UTF8, "application/json");
            var message = GetRequestMessage(HttpMethod.Post, resourceName, content);

            return await GetDataAsync<Bearer>(message);
        }
    }
}
