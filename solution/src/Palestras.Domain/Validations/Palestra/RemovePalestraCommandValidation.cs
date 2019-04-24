using Palestras.Domain.Commands.Palestra;

namespace Palestras.Domain.Validations.Palestra
{
    public class RemovePalestraCommandValidation : PalestraValidation<RemovePalestraCommand>
    {
        public RemovePalestraCommandValidation()
        {
            ValidateId();
        }
    }
}