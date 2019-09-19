using Caliburn.Micro;
using System.Security;
using WindowsParty.Handlers.Contracts;

namespace WindowsParty.ViewModels
{
	public class LoginViewModel : Screen
	{
		#region Constructors

		public LoginViewModel(ILoginHandler loginHandler)
		{
			this.loginHandler = loginHandler;
		}

		#endregion

		#region Properties

		private ILoginHandler loginHandler;

		#endregion

		#region Fields

		public string Username { get; set; }
		public string Password { get; set; }

		#endregion

		#region Methods

		public void Login()
		{
			var shellVM = this.Parent as ShellViewModel;
			shellVM.IsBusy = true;
			App.Current.Dispatcher.Invoke(() =>
			{
				loginHandler.Login(Username, Password).Wait();
				shellVM.NavigateToServersList();
			});
		}

		#endregion
	}
}
