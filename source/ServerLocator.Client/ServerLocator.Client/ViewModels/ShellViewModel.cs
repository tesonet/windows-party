using Caliburn.Micro;

namespace ServerLocator.Client.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<ChangeViewMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            ShowLogin();
        }

        public void Handle(ChangeViewMessage message)
        {
            var viewModel = IoC.GetInstance(message.ViewModelType, null);
            ActivateItem(viewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            this.eventAggregator.Unsubscribe(this);
        }

        private void ShowLogin()
        {
            eventAggregator.PublishOnUIThread(new ChangeViewMessage(typeof(LoginViewModel)));
        }
    }
}
