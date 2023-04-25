using Betisa.Framework.Core.EventBus.Abstractions;
using Betisa.Framework.Core.EventBus.Events;

namespace Publisher.Sample
{
    public interface IPublisherEventService
    {
        void Send<T>(T @event) where T : IntegrationEvent;
    }

    public class PublisherEventService : IPublisherEventService
    {
        private readonly ILogger<PublisherEventService> logger;
        private readonly IEventBus eventBus;

        public PublisherEventService(ILogger<PublisherEventService> logger,
            IEventBus eventBus)
        {
            this.logger = logger;
            this.eventBus = eventBus;
        }

        public void Send<T>(T @event) where T : IntegrationEvent
        {
            logger.LogInformation("--------- {ApplicationContext} is preparing event with id {eventId} ",
                "", @event.Id);
            try
            {
                eventBus.Publish(@event);

                logger.LogInformation("--------- {ApplicationContext} has handled event with id {eventId} ",
                    "", @event.Id);
            }
            catch (System.Exception exp)
            {
                logger.LogCritical(exp,
                    "--------- {ApplicationContext} is preparing event with id {eventId} with Content " +
                    " @{event}",
                    "", @event.Id, @event);
            }
        }
    }
}
