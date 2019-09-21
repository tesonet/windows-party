using Caliburn.Micro;
using System.Threading.Tasks;
using WindowsParty.Constants;
using WindowsParty.Handlers.Contracts;
using WindowsParty.Models;

namespace WindowsParty.Handlers
{
	public class LoginHandler : HandlerBase, ILoginHandler
	{
		#region Properties
		protected override string Endpoint => "tokens";

		#endregion

		#region Methods

		public async Task<bool> Login(string username, string password)
		{
			var token = await Post<LoginResponse>(new Login() { Username = username, Password = password });
			if (token == null)
				return false;

			App.Current.Properties[DefaultValues.TOKEN] = token.Token;

			return true;
		}

		#endregion
	}
}
