using MahApps.Metro.Controls;
using System.ComponentModel.Composition;

namespace PrimsModulityWithWPF
{
    [Export(typeof(Shell))]
    public partial class Shell : MetroWindow
    {
        #region ctor()
        public Shell()
        {
            InitializeComponent();
        }
        #endregion
    }
}
