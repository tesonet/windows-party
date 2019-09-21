using System.Threading.Tasks;

namespace WindowsParty.Handlers.Contracts
{
	public interface ILoginHandler
	{
		Task<bool> Login(string username, string password);
	}
}
