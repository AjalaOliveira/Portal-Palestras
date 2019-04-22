using System;
using Palestras.Domain.Validations.Palestrante;

namespace Palestras.Domain.Commands.Palestrante
{
    public class RegisterNewPalestranteCommand : PalestranteCommand
    {
        public RegisterNewPalestranteCommand(string nome, string miniBio, string url)
        {
            Nome = nome;
            MiniBio = miniBio;
            Url = url;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewPalestranteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}