using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class UpdatePalestraCommand : PalestraCommand
    {
        public UpdatePalestraCommand(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
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