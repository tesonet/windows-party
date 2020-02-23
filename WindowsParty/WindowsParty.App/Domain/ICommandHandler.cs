
using System.Threading.Tasks;

namespace WindowsParty.App.Domain
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
