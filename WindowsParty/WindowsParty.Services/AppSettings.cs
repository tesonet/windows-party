using System.Configuration;
using WindowsParty.Common.Interfaces;

namespace WindowsParty.Services
{
    public class AppSettings : IAppSettings
    {
        public string TokenUrl => ConfigurationManager.AppSettings["TokenUrl"];
        public string ServersUrl => ConfigurationManager.AppSettings["ServersUrl"];
    }
}
