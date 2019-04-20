using System;
using Palestras.Domain.Validations.Palestrante;

namespace Palestras.Domain.Commands.Palestrante
{
    public class RemovePalestranteCommand : PalestranteCommand
    {
        public RemovePalestranteCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePalestranteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}