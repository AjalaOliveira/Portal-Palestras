using System;
using Palestras.Domain.Core.Events;

namespace Palestras.Domain.Events.Palestrante
{
    public class PalestranteRegisteredEvent : Event
    {
        public PalestranteRegisteredEvent(Guid id, string nome, string miniBio, string url)
        {
            Id = id;
            Nome = nome;
            MiniBio = miniBio;
            Url = url;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Nome { get; private set; }

        public string MiniBio { get; private set; }

        public string Url { get; private set; }
    }
}