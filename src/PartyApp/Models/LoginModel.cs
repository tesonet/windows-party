using System.Threading;
using System.Threading.Tasks;
using PartyApp.Services;
using PartyApp.Utilities;

namespace PartyApp.Models
{
	public class LoginModel : ILoginModel
	{
		private readonly IAuthorizer _authorizer;

		public LoginModel(IAuthorizer authorizer)
		{
			Guard.NotNull(authorizer, nameof(authorizer));
			_authorizer = authorizer;
		}

		public async Task<AuthorizationResult> Login(
			string userName,
			string password,
			CancellationToken cancellationToken)
		{
			return await _authorizer.AuthorizeAsync(
				userName, password, cancellationToken);
		}
	}
}
