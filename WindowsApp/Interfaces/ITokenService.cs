namespace WPFApp.Interfaces
{
    public interface ITokenService
    {
        string Token { get; set; }
        void SaveToken(string token);
    }
}
