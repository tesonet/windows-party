using Caliburn.Micro;

namespace WindowsParty.App.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}
