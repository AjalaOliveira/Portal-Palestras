using System;
using Palestras.Domain.Core.Commands;

namespace Palestras.Domain.Commands.Palestra
{
    public abstract class PalestraCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Titulo { get; protected set; }

        public string Descricao { get; protected set; }

        public DateTime Data { get; protected set; }

        public Guid PalestranteId { get; protected set; }
    }
}