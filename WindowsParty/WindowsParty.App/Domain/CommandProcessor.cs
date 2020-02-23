using Caliburn.Micro;
using System.Threading.Tasks;

namespace WindowsParty.App.Domain
{
    public interface ICommandProcessor
    {
        Task ProcessAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public class CommandProcessor : ICommandProcessor
    {
        public Task ProcessAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<TCommand>);

            var handlerInstance = (ICommandHandler<TCommand>)IoC.GetInstance(handlerType, null);

            return handlerInstance.Handle(command);
        }
    }
}
