using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Net.Http.Headers;

namespace TesonetTestTask
{
    class ConnectionMgt
    {
        public string GetToken(string url, string user, string pass)
        {

            try
            {
                JObject oLogin = JObject.FromObject(new
                {
                    username = user,
                    password = pass
                });

                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(1, 1, 1);

                var content = new StringContent(oLogin.ToString(), Encoding.UTF8, "application/json");

                System.Threading.Tasks.Task<HttpResponseMessage> responseTask = httpClient.PostAsync("https://playground.tesonet.lt/v1/tokens", content);
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;

                System.Threading.Tasks.Task<String> strContentTask = response.Content.ReadAsStringAsync();
                strContentTask.Wait();                
                string strContent = strContentTask.Result;

                JObject tobj = JObject.Parse(strContent);

                if (!String.IsNullOrWhiteSpace((string)tobj["message"]))
                {
                    MessageBox.Show((string)tobj["message"],"Warning");
                    return "";
                }
                
                return (string)tobj["token"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somethink Wrong Error: " + ex.Message);
                return "";
            }
        }

        public string GetServerList(string url)
        {

            try
            {                              
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(1, 1, 1);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppContext.Token);

                System.Threading.Tasks.Task<HttpResponseMessage> responseTask = httpClient.GetAsync(url);
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;

                System.Threading.Tasks.Task<String> strContentTask = response.Content.ReadAsStringAsync();

                strContentTask.Wait();
                string strContent = strContentTask.Result;
                
                return strContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somethink Wrong Error: " + ex.Message);
                return "";
            }
        }

    }
}
