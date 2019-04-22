using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class RegisterNewPalestraCommand : PalestraCommand
    {
        public RegisterNewPalestraCommand(string titulo, string descricao, DateTime data, Guid palestranteId)
        {
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            PalestranteId = palestranteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewPalestraCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}