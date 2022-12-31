using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroTest1.EventBus.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
