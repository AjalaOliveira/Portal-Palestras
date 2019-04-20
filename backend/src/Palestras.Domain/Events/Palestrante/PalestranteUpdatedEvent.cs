using System;
using Palestras.Domain.Core.Events;

namespace Palestras.Domain.Events.Palestrante
{
    public class PalestranteUpdatedEvent : Event
    {
        public PalestranteUpdatedEvent(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}