using System;
using Mapster;
using MicroTest1.EventBus.EventBus.Abstractions;
using MicroTest1.EventBus.EventBus.Events;

namespace MicroTest1
{
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
                "AppName", @event.Id);
            try
            {
                eventBus.Publish(@event);

                logger.LogInformation("--------- {ApplicationContext} has handled event with id {eventId} ",
                    "AppName", @event.Id);
            }
            catch (System.Exception exp)
            {
                logger.LogCritical(exp,
                    "--------- {ApplicationContext} is preparing event with id {eventId} with Content " +
                    " @{event}",
                    "AppName", @event.Id, @event);
            }
        }
        public async Task SendAsync<T>(T @event) where T : IntegrationEvent
        {
            Send(@event);
            await Task.CompletedTask;
        }
    }

    public interface IActivityEventService
    {
        void Send<T>(T @event) where T : IntegrationEvent;
        Task SendAsync<T>(T @event) where T : IntegrationEvent;

    }

    public class ChatDeletedEvent : IntegrationEvent
    {
        public ChatDeletedEvent(string item)
        {
            this.item = item;
        }

        public string item { get; set; }
    }



    public class CreateUserEvent : IntegrationEvent
    {
        public CreateUserEvent(string ffirstName ,string lastname , string phoneN)
        {
            this.firstNAme = ffirstName;
            this.LastNAme= lastname;
            this.Phonenumber = phoneN;
        }

        public string firstNAme { get; set; }
        public string LastNAme { get; set; }
        public string Phonenumber { get; set; }
    }



    

    public class NotifyDto
    {
        public NotifyDto()
        {

        }


        public string ActorId { get; set; }
        public int? ActorTypeId { get; set; }
        public string AffectedUserId { get; set; }
        public int? AffectedUserTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InDriectObjectId { get; set; }
        public int? InDriectObjectTypeId { get; set; }
        public string ObjectId { get; set; }
        public int? ObjectTypeId { get; set; }
        public int Verb { get; set; }
        public string Id { get; set; }

    }



    
}


