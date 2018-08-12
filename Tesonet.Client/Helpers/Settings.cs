namespace Tesonet.Client.Helpers
{
    public class Settings : ISettings
    {
        public string ServerAuthUrl
        {
            get => Properties.Settings.Default.ServerAuthUrl;
            set
            {
                Properties.Settings.Default.ServerAuthUrl = value;
                Properties.Settings.Default.Save();
            }
        }

        public string ServerServersUrl
        {
            get => Properties.Settings.Default.ServerServersUrl;
            set
            {
                Properties.Settings.Default.ServerServersUrl = value;
                Properties.Settings.Default.Save();
            }
        }
        public string AuthToken { get; set; }
    }
}