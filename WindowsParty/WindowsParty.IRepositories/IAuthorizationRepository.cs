namespace WindowsParty.IRepositories
{
    public interface IAuthorizationRepository
    {
        string GetAccessToken(string username, string password);        
    }
}
