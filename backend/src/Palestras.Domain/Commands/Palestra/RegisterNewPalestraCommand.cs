using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class RegisterNewPalestraCommand : PalestraCommand
    {
        public RegisterNewPalestraCommand(string titulo, string email, DateTime birthDate)
        {
            Titulo = titulo;
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