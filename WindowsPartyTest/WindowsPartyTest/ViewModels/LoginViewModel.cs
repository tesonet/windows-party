using Caliburn.Micro;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsPartyTest.Client.Base;
using WindowsPartyTest.Client.Services.Interfaces;
using WindowsPartyTest.Logic;
using WindowsPartyTest.Messages;
using WindowsPartyTest.Models;
using WindowsPartyTest.ViewModels.Interfaces;
using WindowsPartyTest.Views.Interfaces;

namespace WindowsPartyTest.ViewModels
{
    public class LoginViewModel : Screen, ILoginViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();
        private readonly IEventAggregator _eventAggregator;
        private LoginLogic _logic = null;
        private ILoginService _service = null;
        private string _userID = string.Empty;

        public LoginViewModel(IEventAggregator eventAggregator, ILoginService loginService)
        {
            _eventAggregator = eventAggregator;
            _service = loginService;
            _logic = new LoginLogic(_service);
        }

        public string UserID
        {
            get { return _userID; }
            set 
            {
                _userID = value;
                NotifyOfPropertyChange();
            }
        }
        public SecureString Password { private get; set; }

        public void Login()
        {
            if (_logic.Login(new UserData() { UserID = UserID, Password = Password }))
            {
                _eventAggregator.PublishOnUIThread(new SuccessfulLoginMessage());
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
            ClearPassword();
        }

        public bool Validate()
        {
            bool isValid = !string.IsNullOrEmpty(UserID);
            bool contains = _validationErrors.ContainsKey(nameof(UserID));
            if (!isValid && !contains)
                _validationErrors.Add(nameof(UserID), "Mandatory field!");
            else if (isValid && contains)
                _validationErrors.Remove(nameof(UserID));

            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(UserID)));
            return isValid; 
        }
        public bool HasErrors => _validationErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            string message;
            if (_validationErrors.TryGetValue(propertyName, out message))
                return new List<string> { message };

            return null;
        }

        public void ClearData()
        {
            ClearPassword();
        }
        public void ClearPassword()
        {
            Password?.Clear();
            ILoginHandler handler = IoC.Get<ILoginHandler>();
            if(handler != null)
                handler.ClearPassword();
        }
    }
}
