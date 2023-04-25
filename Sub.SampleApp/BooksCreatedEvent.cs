using Betisa.Framework.Core.EventBus.Abstractions;
using Betisa.Framework.Core.EventBus.Events;

namespace Sub.SampleApp
{
    public class BooksCreatedEvent : IntegrationEvent
    {
        public BooksCreatedEvent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
    public class BooksCreatedEventHandler : IIntegrationEventHandler<BooksCreatedEvent>
    {
        private readonly ILogger<BooksCreatedEventHandler> logger;
        private readonly IServiceProvider sp;

        public BooksCreatedEventHandler(
            ILogger<BooksCreatedEventHandler> logger,
            IServiceProvider sp)
        {
            this.logger = logger;
            this.sp = sp;
        }

        public async Task Handle(BooksCreatedEvent @event)
        {
            logger.LogInformation("-----{ApplicationContext} starts handlig event ({eventName}): when" +
                " an event with id({eventId})",
                    "sdcdscd", nameof(BooksCreatedEvent), @event.Id);

            using (var scope = sp.CreateScope())
                try
                {

                }
                catch (Exception e)
                {
                    logger.LogCritical(e, "-----handlig event failed at {ApplicationContext} : when" +
                            " an event with id({eventId}) & name ({eventName}) with content of (@{event} happened)",
                        "Program.AppName", @event.Id, nameof(BooksCreatedEvent), @event);
                }
            await Task.CompletedTask;
        }
    }
}
