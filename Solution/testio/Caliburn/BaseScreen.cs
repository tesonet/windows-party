using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using testio.Core.Logging;
using System.Windows.Input;

namespace testio.Caliburn
{
    public class BaseScreen : Screen
    {
        protected ILogger _logger = null;

        private bool _isBusy = false;
        private string _error = null;

        public BaseScreen(ILogger logger)
        {
            _logger = logger;
        }

        protected void SetBusy()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            IsBusy = true;
            Error = null;
        }

        protected void ProcessException(Exception e)
        { 
            Error = e.Message;
            _logger.LogErrorFormat(e, GetType().ToString());
        }

        protected void SetNotBusy()
        {
            IsBusy = false;
            Mouse.OverrideCursor = null;
        }

        protected virtual void OnBusyChanged()
        {
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
                OnBusyChanged();
            }
        }

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
    }
}
