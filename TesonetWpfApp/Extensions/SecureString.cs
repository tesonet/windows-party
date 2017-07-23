using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TesonetWpfApp.Extensions
{
    public static class SecureStringExtensions
    {
        public static string ToInsecureString(this SecureString secure)
        {
            IntPtr valuePtr = IntPtr.Zero;
            string insecureString;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secure);
                insecureString = Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                secure.Clear();
            }

            return insecureString;
        }
    }
}
