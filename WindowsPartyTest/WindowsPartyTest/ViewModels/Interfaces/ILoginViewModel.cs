using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPartyTest.ViewModels
{
    public interface ILoginViewModel : INotifyPropertyChanged
    {
        string UserID { get; set; }
        SecureString Password { set;}
        void Login();
        bool Validate();
        void ClearData();
    }
}
