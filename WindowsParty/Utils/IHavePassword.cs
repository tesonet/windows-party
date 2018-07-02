
using System.Security;

namespace WindowsParty.Utils
{
    /// <summary>
    /// View can expose password
    /// </summary>
    public interface IHavePassword
    {
        SecureString Password { get; }
    }
}
