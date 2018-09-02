
namespace CaliburnMicro.LoginTestExternal.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;
    using Newtonsoft.Json.Serialization;
    using Caliburn.Micro;
    using CaliburnMicro.LoginTestExternal.Framework;


   
    public class MockLoginService : ILoginService
    {
       
        private Dictionary<string, string> users;
        private readonly ILog Log;
        private string GetToken(string user, string password,string address)
        {
            string token = string.Empty;
            var body = new List<KeyValuePair<string, string>>
                      {

                        new KeyValuePair<string, string>("username", user),
                        new KeyValuePair<string, string> ("password", password)
                    };
            var content = new FormUrlEncodedContent(body);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync(address, content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var dict_token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result, new JsonSerializerSettings
                {
                    Error = delegate (object sender, ErrorEventArgs args)
                    {

                        Log.Error(args.ErrorContext.Error);
                        args.ErrorContext.Handled = true;
                    }
                });
                if(dict_token.ContainsKey("token"))
                {

                    token =   dict_token["token"];

                }

                else
                {
                    Log.Warn(String.Concat("Wrong credentials:Login time:", DateTime.Now));
                }
                
            }

            return token;
        }
       
        public MockLoginService()
        {
           
            LogManager.GetLog = type => new SimpleFileLogger(type, () => CanDeleteLogFile());
            Log = LogManager.GetLog(typeof(MockLoginService));
        }

        private  bool CanDeleteLogFile()
        {
            bool output=true;
           
           
            return output;
        }


      
        public  string ValidateUser(string username, string password, string address)
        {
            string token = string.Empty;
            token=GetToken(username, password, address);
            return token;
        }

        public List<Server> GetList(string token,string url)
        {
            List<Server> list = new List<Server>();
            try
            {
                using (var client = new HttpClient())

                {
                    client.DefaultRequestHeaders.Add("Authorization", String.Concat("Bearer ", token));
                    var response1 = client.GetAsync(url).Result;
                    var result = response1.Content.ReadAsStringAsync().Result;
                    var jss = new JavaScriptSerializer();
                    dynamic servers = jss.DeserializeObject(result);

                    foreach (var server in servers)
                    {

                        list.Add(new Server(server["name"].ToString(), server["distance"].ToString()));

                    }
                }
            }

            catch(Exception ex)
            {

                Log.Error(ex);

            }

            return list;
        }
    }
}
