namespace WindowsParty.Ui.ViewModels
{
    using System.Threading.Tasks;

    public interface IViewModel
    {
        Task ActivateAsync(object parameter);
    }
}