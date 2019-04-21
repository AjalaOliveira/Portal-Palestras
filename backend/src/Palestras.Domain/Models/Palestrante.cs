using System;
using Palestras.Domain.Core.Models;

namespace Palestras.Domain.Models
{
    public class Palestrante : Entity
    {
        public Palestrante(Guid id, string nome, string email, DateTime birthDate)
        {
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Palestrante()
        {
        }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}