using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;

namespace Tesonet.Views
{
    /// <summary>
    /// Interaction logic for Servers.xaml
    /// </summary>
    public partial class Servers : UserControl
    {
        public Servers(string token)
        {
            InitializeComponent();
            PopulateServers(token);
        }

        private async void PopulateServers(string token)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "servers"))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await RequestManager.httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                    RenderServers(response.Content.ReadAsStringAsync().Result);
                else throw new UserAuthenticationException("Token error", "Server error");
            }
        }

        private void RenderServers(string result)
        {
            var serversList = JsonConvert.DeserializeObject<List<Server>>(result);
            serversGrid.ItemsSource = serversList;
        }
    }

    public class Server
    {
        public string name { get; set; }
        public int distance { get; set; }
    }
}
