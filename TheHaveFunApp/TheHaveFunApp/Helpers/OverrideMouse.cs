using System;
using System.Windows.Input;

namespace TheHaveFunApp.Helpers
{
    public class OverrideMouse : IDisposable
    {
        public OverrideMouse()
        {
            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void Dispose()
        {
            Mouse.OverrideCursor = null;
        }
    }
}
