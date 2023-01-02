using System;
using MicroTest1.EventBus.EventBus.Abstractions;
using MicroTest1.EventBus.EventBus.Events;

namespace MicroTest1
{
    //public class ActivityEventService : IActivityEventService
    //{
    //    private readonly ILogger<ActivityEventService> logger;
    //    private readonly IEventBus eventBus;

    //    public ActivityEventService(ILogger<ActivityEventService> logger,
    //        IEventBus eventBus)
    //    {
    //        this.logger = logger;
    //        this.eventBus = eventBus;
    //    }

    //    public void Send<T>(T @event) where T : IntegrationEvent
    //    {
    //        logger.LogInformation("--------- {ApplicationContext} is preparing event with id {eventId} ",
    //            "AppName", @event.Id);
    //        try
    //        {
    //            eventBus.Publish(@event);

    //            logger.LogInformation("--------- {ApplicationContext} has handled event with id {eventId} ",
    //                "AppName", @event.Id);
    //        }
    //        catch (System.Exception exp)
    //        {
    //            logger.LogCritical(exp,
    //                "--------- {ApplicationContext} is preparing event with id {eventId} with Content " +
    //                " @{event}",
    //                "AppName", @event.Id, @event);
    //        }
    //    }
    //    public async Task SendAsync<T>(T @event) where T : IntegrationEvent
    //    {
    //        Send(@event);
    //        await Task.CompletedTask;
    //    }
    //}

    public interface IActivityEventService
    {
        void Send<T>(T @event) where T : IntegrationEvent;
        Task SendAsync<T>(T @event) where T : IntegrationEvent;

    }
    public class ActivityEventService : IActivityEventService
    {
        private readonly ILogger<ActivityEventService> logger;
        private readonly IEventBus eventBus;

        public ActivityEventService(ILogger<ActivityEventService> logger,
            IEventBus eventBus)
        {
            this.logger = logger;
            this.eventBus = eventBus;
        }

        public void Send<T>(T @event) where T : IntegrationEvent
        {
            logger.LogInformation("--------- {ApplicationContext} is preparing event with id {eventId} ",
                "Program.AppName", @event.Id);
            try
            {
                eventBus.Publish(@event);

                logger.LogInformation("--------- {ApplicationContext} has handled event with id {eventId} ",
                    "Program.AppName", @event.Id);
            }
            catch (System.Exception exp)
            {
                logger.LogCritical(exp,
                    "--------- {ApplicationContext} is preparing event with id {eventId} with Content " +
                    " @{event}",
                    "Program.AppName", @event.Id, @event);
            }
        }
        public async Task SendAsync<T>(T @event) where T : IntegrationEvent
        {
            Send(@event);
            await Task.CompletedTask;
        }

    }

}

