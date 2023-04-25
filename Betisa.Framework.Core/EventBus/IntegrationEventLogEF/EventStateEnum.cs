using System;
using System.Collections.Generic;
using System.Text;

namespace Betisa.Framework.Core.IntegrationEventLogEF
{
    public enum EventStateEnum
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }
}
