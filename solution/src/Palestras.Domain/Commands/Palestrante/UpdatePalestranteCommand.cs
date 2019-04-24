using System;
using Palestras.Domain.Validations.Palestrante;

namespace Palestras.Domain.Commands.Palestrante
{
    public class UpdatePalestranteCommand : PalestranteCommand
    {
        public UpdatePalestranteCommand(Guid id, string nome, string miniBio, string url)
        {
            Id = id;
            Nome = nome;
            MiniBio = miniBio;
            Url = url;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePalestranteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}