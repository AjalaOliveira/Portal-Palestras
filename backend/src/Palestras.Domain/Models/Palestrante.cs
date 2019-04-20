using System;
using Palestras.Domain.Core.Models;

namespace Palestras.Domain.Models
{
    public class Palestrante : Entity
    {
        public Palestrante(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Palestrante()
        {
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}