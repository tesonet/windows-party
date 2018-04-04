using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet.Party.Services
{
    public interface IShellService
    {
        void ShowLogin();
        void LoginComplete();
        object SelectedViewModel { get; }
    }
}
