using System;
using Newtonsoft.Json;

namespace MicroTest1.EventBus.EventBus.Events
{
    public class IntegrationEvent
    {
        private IntegrationEvent ()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent (DateTime? creationDate = null) : this ()
        {

        }

        [JsonConstructor]
        public IntegrationEvent (Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}