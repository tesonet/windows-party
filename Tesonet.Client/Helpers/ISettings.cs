namespace Tesonet.Client.Helpers
{
    public interface ISettings
    {
        string ServerAuthUrl { get; set; }
        string ServerServersUrl { get; set; }
        string AuthToken { get; set; }
    }
}