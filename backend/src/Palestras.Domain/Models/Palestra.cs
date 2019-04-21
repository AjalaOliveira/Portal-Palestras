using System;
using Palestras.Domain.Core.Models;

namespace Palestras.Domain.Models
{
    public class Palestra : Entity
    {
        public Palestra(Guid id, string titulo, string email, DateTime birthDate)
        {
            Id = id;
            Titulo = titulo;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Palestra()
        {
        }

        public string Titulo { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}