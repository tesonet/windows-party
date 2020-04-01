using API.Communicator.Models;
using API.Logger;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace API.Communicator
{
    class HttpCommunicator : ICommunicator
    {
        readonly ILogger logger;
        LoginResponseModel token;
        static HttpClient server { get; set; }

        public HttpCommunicator(ILogger logger)
        {
            this.logger = logger;
            server = new HttpClient();
            server.BaseAddress = new Uri("http://playground.tesonet.lt/v1/");
            server.DefaultRequestHeaders.Accept.Clear();
            server.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> LogIn(string userName, PasswordBox password)
        {
            HttpContent content = new StringContent($"{{\"username\": \"{userName}\", \"password\": \"{password.Password}\"}}", Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await server.PostAsync("tokens", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsAsync<LoginResponseModel>();
                    logger.Info("Login successful");
                    return true;
                }
                logger.Error("Login failed with message: " + response.ReasonPhrase);
            }
            return false;
        }

        public async Task<List<ServerInfoModel>> GetServerList()
        {
            server.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            using (HttpResponseMessage response = await server.GetAsync("servers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ServerInfoModel> serverList = await response.Content.ReadAsAsync<List<ServerInfoModel>>();
                    logger.Info("Got server list");
                    return serverList;
                }
                logger.Error(response.ReasonPhrase);
            }
            return new List<ServerInfoModel>();
        }
    }
}
