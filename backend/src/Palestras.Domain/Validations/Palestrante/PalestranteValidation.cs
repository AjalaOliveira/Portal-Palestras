using System;
using FluentValidation;
using Palestras.Domain.Commands.Palestrante;

namespace Palestras.Domain.Validations.Palestrante
{
    public abstract class PalestranteValidation<T> : AbstractValidator<T> where T : PalestranteCommand
    {
        protected void ValidaNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor, verifique se você digitou o nome")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("O Palestrante deve ter 18 anos ou mais");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}