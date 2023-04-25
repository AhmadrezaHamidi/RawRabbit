using Betisa.Framework.Core.RabbitMq;
using Betisa.Framework.Core.Messages;
using System.Threading.Tasks;

namespace Betisa.Framework.Core.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}