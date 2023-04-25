using System.Threading.Tasks;
using Betisa.Framework.Core.Messages;

namespace Betisa.Framework.Core.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context)
            where TEvent : IEvent;
    }
}