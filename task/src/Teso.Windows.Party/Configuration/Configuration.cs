namespace Teso.Windows.Party.Configuration
{
    public class Configuration :IConfiguration
    {
        public string BaseUrl
        {
            get
            {
                return Properties.Settings.Default.BaseUrl;
            }
        }
    }
}