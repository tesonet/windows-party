using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesonet.Party.Services;
using Unity;

namespace Tesonet.Party.ViewModels
{
    public class ShellVM : ViewModelBase, IShellService
    {
        public ShellVM(IUnityContainer container) : base(container)
        {
            ShowLogin();
        }

        private object _SelectedViewModel;
        public object SelectedViewModel
        {
            get { return _SelectedViewModel; }
            set
            {
                if (_SelectedViewModel != value)
                {
                    _SelectedViewModel = value;
                    OnPropertyChanged(nameof(SelectedViewModel));
                }
            }
        }

        public void ShowLogin()
        {
            SelectedViewModel = container.Resolve<LoginVM>();
        }

        public void LoginComplete()
        {
            SelectedViewModel = container.Resolve<ServersVM>();
        }
    }
}
