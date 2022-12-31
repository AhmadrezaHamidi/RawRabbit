using System;
using MicroTest1.BaseModels;
using Newtonsoft.Json;

namespace MicroTest1.EventBus.Events
{
    public class IntegrationEvent
    {
        private IntegrationEvent ()
        {
            Id = IDentifiable.New;
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent (DateTime? creationDate = null) : this ()
        {

        }

        [JsonConstructor]
        public IntegrationEvent (IDentifiable id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public IDentifiable Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}