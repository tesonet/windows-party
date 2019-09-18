using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsParty.ViewModels
{
	public class LoginViewModel : Screen
	{
		#region Fields

		public string Username { get; set; }
		public string Password { get; set; }

		#endregion

		#region Methods

		public void Login()
		{
			var shellVM = (this.Parent as ShellViewModel);
			shellVM.Login();
		}

		#endregion
	}
}
