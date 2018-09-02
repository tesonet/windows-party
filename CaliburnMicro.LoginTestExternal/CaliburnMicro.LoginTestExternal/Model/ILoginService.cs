
namespace CaliburnMicro.LoginTestExternal.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ILoginService
    {
        
        string ValidateUser(string username, string password, string address);
      

        List<Server> GetList(string token, string url);
    }
}
