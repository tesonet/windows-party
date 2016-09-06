using System.Threading;
using System.Threading.Tasks;
using PartyApp.Services;

namespace PartyApp.Models
{
	public interface ILoginModel
	{
		Task<AuthorizationResult> Login(
			string userName,
			string password,
			CancellationToken cancellationToken);
	}
}