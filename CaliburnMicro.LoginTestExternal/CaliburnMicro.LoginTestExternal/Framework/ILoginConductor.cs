
namespace CaliburnMicro.LoginTestExternal.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Caliburn.Micro;

    /// <summary>
    /// Collection of events to handle
    /// </summary>
    public interface ILoginConductor : IHandle<LoginEvent>, IHandle<LogoutEvent>, IHandle<ExitEvent>
    {
    }
}
