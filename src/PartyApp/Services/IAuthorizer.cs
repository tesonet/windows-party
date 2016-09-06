using System.Threading;
using System.Threading.Tasks;

namespace PartyApp.Services
{
	public interface IAuthorizer
	{
		Task<AuthorizationResult> AuthorizeAsync(
			string userName,
			string password,
			CancellationToken cancellationToken);
	}
}