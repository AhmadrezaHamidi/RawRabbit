using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Betisa.Framework.Core.EventBus.Events;

namespace Betisa.Framework.Core.IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(string transactionId);
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
        Task MarkEventAsPublishedAsync(string eventId);
        Task MarkEventAsInProgressAsync(string eventId);
        Task MarkEventAsFailedAsync(string eventId);
    }
}
