using System;
using System.Collections.Generic;
using Palestras.Domain.Core.Events;

namespace Palestras.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}