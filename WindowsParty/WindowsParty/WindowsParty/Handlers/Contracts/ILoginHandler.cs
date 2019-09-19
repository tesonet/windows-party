using System.Threading.Tasks;

namespace WindowsParty.Handlers.Contracts
{
	public interface ILoginHandler
	{
		Task Login(string username, string password);
	}
}
