using PartyApp.Utilities;

namespace PartyApp.Services
{
	public class AuthorizationResult
	{
		public AuthorizationResult(AuthorizationToken token)
		{
			Guard.NotNull(token, nameof(token));
			Token = token;
			Success = true;
		}

		public AuthorizationResult(string errorMessage)
		{
			ErrorMessage = errorMessage ?? "";
		}

		public bool Success { get; }

		public AuthorizationToken Token { get; }

		public string ErrorMessage { get; }
	}
}
