using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsParty.ViewModels
{
	public class ShellViewModel : Conductor<object>
	{
		#region Constructors
		
		public ShellViewModel()
		{
			LoadDefault();
		}

		#endregion

		#region Methods
		public void LoadDefault()
		{
			ActivateItem(new LoginViewModel());
		}

		public void Login()
		{
			ActivateItem(new ServerListViewModel());
		}

		#endregion
	}
}
