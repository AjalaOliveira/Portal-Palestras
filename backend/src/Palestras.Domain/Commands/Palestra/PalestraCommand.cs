using System;
using Palestras.Domain.Core.Commands;

namespace Palestras.Domain.Commands.Palestra
{
    public abstract class PalestraCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Titulo { get; protected set; }

        public string Email { get; protected set; }

        public DateTime BirthDate { get; protected set; }
    }
}