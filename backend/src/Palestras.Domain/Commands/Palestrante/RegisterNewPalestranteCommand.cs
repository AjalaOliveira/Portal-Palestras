using System;
using Palestras.Domain.Validations.Palestrante;

namespace Palestras.Domain.Commands.Palestrante
{
    public class RegisterNewPalestranteCommand : PalestranteCommand
    {
        public RegisterNewPalestranteCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewPalestranteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}