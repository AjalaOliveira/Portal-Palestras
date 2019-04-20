using System;
using Palestras.Domain.Validations.Palestra;

namespace Palestras.Domain.Commands.Palestra
{
    public class RemovePalestraCommand : PalestraCommand
    {
        public RemovePalestraCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePalestraCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}