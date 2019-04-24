using System;
using Palestras.Domain.Commands.Palestra;
using FluentValidation;

namespace Palestras.Domain.Validations.Palestra
{
    public abstract class PalestraValidation<T> : AbstractValidator<T> where T : PalestraCommand
    {
        protected void ValidaTitulo()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Por favor, verifique se você digitou o titulo")
                .Length(2, 150).WithMessage("O titulo deve ter entre 2 e 150 caracteres");
        }

        protected void ValidateData()
        {
            RuleFor(c => c.Data)
                .NotEmpty()
                .Must(EhValida)
                .WithMessage("A Palestra deve ter a data de hoje ou data futura.");
        }

        protected void ValidateDescricao()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Por favor, verifique se você digitou a descrição")
                .Length(2, 150).WithMessage("A descrição deve ter entre 2 e 150 caracteres");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateIdPalestrante()
        {
            RuleFor(c => c.PalestranteId)
                .NotEqual(Guid.Empty);
        }

        protected static bool EhValida(DateTime data)
        {
            return data >= DateTime.Now.Date;
        }
    }
}