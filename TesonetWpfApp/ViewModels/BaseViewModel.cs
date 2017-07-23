using Prism.Mvvm;
using Prism.Regions;

namespace TesonetWpfApp.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public string Title => "Tesonet";
        #endregion

        protected const string CONTENT_REGION = "ContentRegion";

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }
        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }
    }
}