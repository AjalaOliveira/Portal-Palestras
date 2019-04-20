using System;
using Palestras.Domain.Core.Commands;

namespace Palestras.Domain.Commands.Palestrante
{
    public abstract class PalestranteCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public DateTime BirthDate { get; protected set; }
    }
}