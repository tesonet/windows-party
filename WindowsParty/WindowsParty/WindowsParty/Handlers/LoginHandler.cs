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

		#region Constructors

		public LoginHandler(SimpleContainer container)
			: base(container)
		{ }

		#endregion

		#region Methods

		public async Task Login(string username, string password)
		{
			var token = await Post<LoginResponse>(new Login() { Username = username, Password = password });
			App.Current.Properties[DefaultValues.TOKEN] = token.Token;
		}

		#endregion
	}
}
