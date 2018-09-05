using LoginModule.ViewModel;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace LoginModule.View
{
    [Export("LoginView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class LoginView : MetroContentControl
    {
        [ImportingConstructor]
        public LoginView(LoginViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
