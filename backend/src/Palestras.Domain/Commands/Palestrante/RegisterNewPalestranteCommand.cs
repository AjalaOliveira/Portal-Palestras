using System;
using Palestras.Domain.Validations.Palestrante;

namespace Palestras.Domain.Commands.Palestrante
{
    public class RegisterNewPalestranteCommand : PalestranteCommand
    {
        public RegisterNewPalestranteCommand(string nome, string email, DateTime birthDate)
        {
            Nome = nome;
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