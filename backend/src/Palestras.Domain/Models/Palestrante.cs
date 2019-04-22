using System;
using System.Collections.Generic;
using Palestras.Domain.Core.Models;

namespace Palestras.Domain.Models
{
    public class Palestrante : Entity
    {
        public Palestrante(Guid id, string nome, string miniBio, string url)
        {
            Id = id;
            Nome = nome;
            MiniBio = miniBio;
            Url = url;
        }

        // Empty constructor for EF
        protected Palestrante()
        {
        }

        public string Nome { get; private set; }

        public string MiniBio { get; private set; }

        public string Url { get; private set; }

        public virtual ICollection<Palestra> Palestras { get; set; }
    }
}