﻿using System;
using Palestras.Domain.Validations.Palestrante;

namespace Palestras.Domain.Commands.Palestrante
{
    public class UpdatePalestranteCommand : PalestranteCommand
    {
        public UpdatePalestranteCommand(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePalestranteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}