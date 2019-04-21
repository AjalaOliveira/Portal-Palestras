using Palestras.Domain.Commands.Palestrante;

namespace Palestras.Domain.Validations.Palestrante
{
    public class UpdatePalestranteCommandValidation : PalestranteValidation<UpdatePalestranteCommand>
    {
        public UpdatePalestranteCommandValidation()
        {
            ValidateId();
            ValidaNome();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}