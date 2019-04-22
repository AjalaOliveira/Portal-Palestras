using System;
using Palestras.Domain.Core.Commands;

namespace Palestras.Domain.Commands.Palestrante
{
    public abstract class PalestranteCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Nome { get; protected set; }

        public string MiniBio { get; protected set; }

        public string Url { get; protected set; }
    }
}