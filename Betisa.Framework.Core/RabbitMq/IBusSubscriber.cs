using System;
using Betisa.Framework.Core.Types;
using Betisa.Framework.Core.Messages;

namespace Betisa.Framework.Core.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, MHException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, MHException, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}
