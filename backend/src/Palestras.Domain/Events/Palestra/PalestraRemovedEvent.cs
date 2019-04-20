using System;
using Palestras.Domain.Core.Events;

namespace Palestras.Domain.Events.Palestra
{
    public class PalestraRemovedEvent : Event
    {
        public PalestraRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}