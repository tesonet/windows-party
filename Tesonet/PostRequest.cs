using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tesonet
{

    class Token
    {
        public string token;
    }

    public class Server
    {
        public string name { get; set; }
        public int distance { get; set; }
    }

    class PostRequest
    {
        private static readonly HttpClient client = new HttpClient();
        private DataBinding dataBind;
        private IModeler parent;

        public PostRequest(DataBinding data, IModeler parent)
        {
            dataBind = data;
            this.parent = parent;
        }

        public async void GenerateRequest()
        {
            Dictionary<string, string> _contentValues =  new Dictionary<string, string>();
            _contentValues.Add("username", dataBind.UserName);
            _contentValues.Add("password", dataBind.Password);
            var content = new FormUrlEncodedContent(_contentValues);
            var response = await client.PostAsync("http://playground.tesonet.lt/v1/tokens", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Issue with request check username/password");
            }
            var responseString = await response.Content.ReadAsStringAsync();
            Token token = JsonConvert.DeserializeObject<Token>(responseString);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.token);
            var servers = await client.GetAsync("http://playground.tesonet.lt/v1/servers");
            var serverResponse = await servers.Content.ReadAsStringAsync();
            parent.SetListToDataBind(JsonConvert.DeserializeObject<List<Server>>(serverResponse));
        }
    }
}
