using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPartyApp.Model
{
    public interface ILoginValidator
    {
        void ValidateUserNamePassword(Credentials credentials);
        void ValidateResponse(AuthToken response);
    }

    public class LoginValidator: ILoginValidator
    {
        public void ValidateUserNamePassword(Credentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Password))
            {
                throw new Exception("User Name or Password can not be empty");
            }
        }

        public void ValidateResponse(AuthToken response)
        {
            if (response == null ||  string.IsNullOrEmpty(response.Token))
            {
                throw new Exception("Incorrect Username or Password");
            }
        }
    }
}
