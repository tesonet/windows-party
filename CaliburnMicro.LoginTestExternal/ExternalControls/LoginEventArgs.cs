
namespace ExternalControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    
    public class LoginEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginEventArgs"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public LoginEventArgs(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

       
        public string Username { get; set; }

       
        public string Password { get; set; }
    }
}
