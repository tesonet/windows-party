using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsParty.Models;

namespace WindowsParty.Services
{
	public interface IServersService
	{
		Task<bool> AuthorizeAsync(string username, string password);

		Task<ICollection<Server>> GetServersAsync();
	}
}