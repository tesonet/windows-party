namespace WindowsParty.Ui.Services
{
    using System.Windows.Controls;

    public interface IPageNavigationService
    {
        object Parameter { get; }

        void NavigateTo<T>(object parameter = null) where T : Page;
    }
}