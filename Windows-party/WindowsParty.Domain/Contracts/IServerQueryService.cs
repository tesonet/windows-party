namespace WindowsParty.Domain.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WindowsParty.Domain.Entities;
    using WindowsParty.Domain.Models;

    public interface IServerQueryService
    {
        Task<IList<Server>> GetAsync(TokenResult token);
    }
}