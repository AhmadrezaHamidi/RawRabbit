using Betisa.Framework.Core.RabbitMq;
using Betisa.Framework.Core.Messages;
using System.Threading.Tasks;

namespace Betisa.Framework.Core.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}