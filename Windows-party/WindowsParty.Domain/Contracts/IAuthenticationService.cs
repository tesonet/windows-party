namespace WindowsParty.Domain.Contracts
{
    using System.Threading.Tasks;
    using WindowsParty.Domain.Models;

    public interface IAuthenticationService
    {
        Task<TokenResult> LogInAsync(Credentials credentials);
    }
}