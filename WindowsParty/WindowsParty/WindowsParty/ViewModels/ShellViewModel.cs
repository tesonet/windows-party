using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsParty.ViewModels
{
	public class ShellViewModel : Conductor<object>
	{
		#region Properties

		private SimpleContainer container;
		private bool isBusy = false;
		public bool IsBusy
		{
			get => isBusy;
			set
			{
				isBusy = value;
				NotifyOfPropertyChange(() => BusyVisibility);
			}
		}
		public Visibility BusyVisibility
		{
			get { return IsBusy ? Visibility.Visible : Visibility.Collapsed; }
		}

		#endregion

		#region Constructors

		public ShellViewModel()
		{
			LoadDefault();
		}

		#endregion

		#region Methods

		public void LoadDefault()
		{
			ActivateItem(IoC.Get<LoginViewModel>());
		}

		public void NavigateToServersList()
		{
			ActivateItem(IoC.Get<ServerListViewModel>());
		}

		#endregion
	}
}
