using System;
using Newtonsoft.Json;

namespace Betisa.Framework.Core.EventBus.Events
{
    public class IntegrationEvent
    {
        private IntegrationEvent ()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent (DateTime? creationDate = null) : this ()
        {

        }

        [JsonConstructor]
        public IntegrationEvent (string id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public string Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}