using System.Security;
using Xceed.Wpf.Toolkit;

namespace TesonetWpfApp.Utils
{
    public class WatermarkPasswordBoxWrapper : IWrappedParameter<SecureString>
    {
        private readonly WatermarkPasswordBox _passwordBox;

        public SecureString Value => _passwordBox != null ? _passwordBox.SecurePassword : new SecureString();

        public WatermarkPasswordBoxWrapper(WatermarkPasswordBox passwordBox)
        {
            _passwordBox = passwordBox;
        }
    }
}