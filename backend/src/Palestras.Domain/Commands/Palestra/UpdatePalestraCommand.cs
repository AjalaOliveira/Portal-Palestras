using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class UpdatePalestraCommand : PalestraCommand
    {
        public UpdatePalestraCommand(Guid id, string titulo, string descricao, DateTime data, Guid palestranteId)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            PalestranteId = palestranteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePalestraCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}