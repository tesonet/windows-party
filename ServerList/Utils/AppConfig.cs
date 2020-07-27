using ServerList.Interfaces;
using System.Configuration;

namespace ServerList.Utils
{
    public class AppConfig : IConfig
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
