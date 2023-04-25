using Betisa.Framework.Core.EventBus.Abstractions;
using Betisa.Framework.Core.EventBus.Events;
using Microsoft.EntityFrameworkCore.Internal;

namespace Publisher.Sample
{
    public class BooksCreatedEvent : IntegrationEvent
    {
        public BooksCreatedEvent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

   
}
