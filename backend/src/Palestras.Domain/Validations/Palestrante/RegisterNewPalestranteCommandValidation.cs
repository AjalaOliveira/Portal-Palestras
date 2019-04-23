using Palestras.Domain.Commands.Palestrante;

namespace Palestras.Domain.Validations.Palestrante
{
    public class RegisterNewPalestranteCommandValidation : PalestranteValidation<RegisterNewPalestranteCommand>
    {
        public RegisterNewPalestranteCommandValidation()
        {
            ValidaNome();
            ValidateMiniBio();
            ValidateUrl();
        }
    }
}