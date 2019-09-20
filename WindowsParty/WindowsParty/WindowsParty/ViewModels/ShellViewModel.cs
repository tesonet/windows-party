using Caliburn.Micro;
using System;

namespace WindowsParty.ViewModels
{
	public class ShellViewModel : Conductor<Screen>, IHandle<Type>
	{
		#region Constructors

		public ShellViewModel(IEventAggregator eventAggregator)
		{
			eventAggregator.Subscribe(this);
		}

		#endregion

		#region Methods

		protected override void OnInitialize()
		{
			base.OnInitialize();
			ActivateItem(IoC.Get<LoginViewModel>());
		}

		public void Handle(Type screenType)
		{
			if (screenType == typeof(LoginViewModel))
				ActivateItem(IoC.Get<LoginViewModel>());
			else
				ActivateItem(IoC.Get<ServerListViewModel>());
		}


		#endregion
	}
}
