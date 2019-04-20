using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class RegisterNewPalestraCommand : PalestraCommand
    {
        public RegisterNewPalestraCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewPalestraCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}