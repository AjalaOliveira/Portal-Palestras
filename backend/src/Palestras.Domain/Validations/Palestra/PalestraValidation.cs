using System;
using Palestras.Domain.Commands.Palestra;
using FluentValidation;

namespace Palestras.Domain.Validations.Palestra
{
    public abstract class PalestraValidation<T> : AbstractValidator<T> where T : PalestraCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Por favor, verifique se você digitou o nome")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("A Palestra deve ter 18 anos ou mais");
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