namespace WindowsParty.IServices
{
    public interface IAuthorizationService
    {
        string GetAccessToken(string username, string password);
    }
}
