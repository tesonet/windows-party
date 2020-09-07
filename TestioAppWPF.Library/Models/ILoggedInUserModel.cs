namespace TestioAppWPF.Library.Models
{
    public interface ILoggedInUserModel
    {
        int Id { get; set; }
        string Token { get; set; }
        string UserName { get; set; }
    }
}