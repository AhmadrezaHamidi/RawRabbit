using System;
using MicroTest1.Messages;
using MicroTest1.Types;

namespace MicroTest1.RabbitMq
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
