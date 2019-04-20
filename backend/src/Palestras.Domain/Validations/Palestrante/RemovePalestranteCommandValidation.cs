using Palestras.Domain.Commands.Palestrante;

namespace Palestras.Domain.Validations.Palestrante
{
    public class RemovePalestranteCommandValidation : PalestranteValidation<RemovePalestranteCommand>
    {
        public RemovePalestranteCommandValidation()
        {
            ValidateId();
        }
    }
}