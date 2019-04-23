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

        protected void ValidateMiniBio()
        {
            RuleFor(c => c.MiniBio)
                .NotEmpty().WithMessage("Por favor, verifique se você digitou a Mini Bio")
                .Length(2, 150).WithMessage("A Mini Bio deve ter entre 2 e 150 caracteres");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateUrl()
        {
            RuleFor(c => c.Url)
                .NotEmpty().WithMessage("Por favor, verifique se você digitou a URL");

        }

        protected static bool HaveMinimumAge(DateTime miniBio)
        {
            return miniBio <= DateTime.Now.AddYears(-18);
        }
    }
}