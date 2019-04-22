using System;
using Palestras.Domain.Core.Events;

namespace Palestras.Domain.Events.Palestra
{
    public class PalestraRegisteredEvent : Event
    {
        public PalestraRegisteredEvent(Guid id, string titulo, string descricao, DateTime data, Guid palestranteId)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            PalestranteId = palestranteId;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public DateTime Data { get; private set; }

        public Guid PalestranteId { get; set; }
    }
}