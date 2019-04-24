using System;
using Palestras.Domain.Core.Models;

namespace Palestras.Domain.Models
{
    public class Palestra : Entity
    {
        public Palestra(Guid id, string titulo, string descricao, DateTime data, Guid palestranteId)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            PalestranteId = palestranteId;
        }

        // Empty constructor for EF
        protected Palestra()
        {
        }

        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public DateTime Data { get; private set; }

        public virtual Palestrante Palestrante { get; set; }

        public Guid PalestranteId { get; set; }
    }
}