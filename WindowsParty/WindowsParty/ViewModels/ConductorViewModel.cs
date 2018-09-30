using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsParty.ViewModels
{
    public class ConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<Type>
    {
        private Dictionary<Type, Screen> screens = new Dictionary<Type, Screen>();
        public ConductorViewModel(LoginViewModel loginViewModel, ServerListViewModel serverViewModel, IEventAggregator eventAggregator)
        {
            screens[typeof(LoginViewModel)] = loginViewModel;
            screens[typeof(ServerListViewModel)] = serverViewModel;
            eventAggregator.Subscribe(this);
            Items.AddRange(screens.Select(v => v.Value));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ActivateItem(screens[typeof(LoginViewModel)]);
        }

        public void Handle(Type screenType)
        {
            if (screens.ContainsKey(screenType)) ActivateItem(screens[screenType]);
            else throw new InvalidOperationException($"Unknown event message: {screenType.Name}.");
        }
    }
}
