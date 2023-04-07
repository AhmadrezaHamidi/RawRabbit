using System;
using MicroTest1.EventBus.EventBus.Abstractions;
using MicroTest1.EventBus.EventBus.Events;

namespace MicroTest1.RabbitMessage
{
    public class ChatDeletedEvent : IntegrationEvent
    {
        public ChatDeletedEvent(string item)
        {
            this.item = item;
        }

        public string item { get; set; }
    }



    public class BlockContactEventHandler : IIntegrationEventHandler<ChatDeletedEvent>
    {
        private readonly ILogger<BlockContactEventHandler> logger;
        public BlockContactEventHandler(ILogger<BlockContactEventHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(ChatDeletedEvent @event)
        {
            logger.LogInformation("-----{ApplicationContext} starts handlig event ({eventName}): when" +
                " an event with id({eventId})",
                "Pdd", nameof(ChatDeletedEvent), @event.Id);
            try
            {
                var ttt = @event.item;
            }
            catch (System.Exception exp)
            {
                logger.LogError(exp, "----handling event failed: {eventId} from {ApplicationContext} with data - ({@event}) ",
                    @event.Id, "pppp", @event);
            }
        }
    }














    public class CreateUserEvent : IntegrationEvent
    {
        public CreateUserEvent(string ffirstName, string lastname, string phoneN)
        {
            this.firstNAme = ffirstName;
            this.LastNAme = lastname;
            this.Phonenumber = phoneN;
        }

        public string firstNAme { get; set; }
        public string LastNAme { get; set; }
        public string Phonenumber { get; set; }
    }



    public class CreateUserEventHandler : IIntegrationEventHandler<CreateUserEvent>
    {
        private readonly ILogger<CreateUserEventHandler> logger;
        public CreateUserEventHandler(ILogger<CreateUserEventHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(CreateUserEvent @event)
        {
            logger.LogInformation("-----{ApplicationContext} starts handlig event ({eventName}): when" +
                " an event with id({eventId})",
                "Pdd", nameof(CreateUserEvent), @event.Id);
            try
            {
                var ttt = @event.Phonenumber;
                var tty = @event.firstNAme;
                var ttts = @event.LastNAme;
            }
            catch (System.Exception exp)
            {
                logger.LogError(exp, "----handling event failed: {eventId} from {ApplicationContext} with data - ({@event}) ",
                    @event.Id, "pppp", @event);
            }
        }
    }
}




