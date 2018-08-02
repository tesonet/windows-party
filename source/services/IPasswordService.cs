using System;

namespace tesonet.windowsparty.services
{
    public interface IPasswordService
    {
        string Password { get; }

        void ClearPassword();
    }
}
