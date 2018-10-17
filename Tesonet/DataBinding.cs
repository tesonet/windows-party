using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet
{
    public class DataBinding : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _userName;
        private string _password;
        private List<Server> _locationTable;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChange("UserName");
                }
            }
        }

        public List<Server> Locationtable
        {
            get
            {
                return _locationTable;
            }
            set
            {
                if (value != _locationTable)
                {
                    _locationTable = value;
                    OnPropertyChange("Locationtable");
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChange("Password");
                }
            }
        }

        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
