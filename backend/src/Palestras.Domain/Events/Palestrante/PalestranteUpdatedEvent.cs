using System;
using Palestras.Domain.Core.Events;

namespace Palestras.Domain.Events.Palestrante
{
    public class PalestranteUpdatedEvent : Event
    {
        public PalestranteUpdatedEvent(Guid id, string nome, string email, DateTime birthDate)
        {
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}