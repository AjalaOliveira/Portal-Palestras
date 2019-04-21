using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class UpdatePalestraCommand : PalestraCommand
    {
        public UpdatePalestraCommand(Guid id, string titulo, string email, DateTime birthDate)
        {
            Id = id;
            Titulo = titulo;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePalestraCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}