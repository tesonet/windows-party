using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsParty.ViewModels;

namespace WindowsParty
{
	public class AppBootstrapper : BootstrapperBase
	{
		#region Constructors

		public AppBootstrapper()
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}

		#endregion
	}
}
