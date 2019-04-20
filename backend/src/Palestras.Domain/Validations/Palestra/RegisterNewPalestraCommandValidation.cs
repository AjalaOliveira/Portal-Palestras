using Palestras.Domain.Commands.Palestra;

namespace Palestras.Domain.Validations.Palestra
{
    public class RegisterNewPalestraCommandValidation : PalestraValidation<RegisterNewPalestraCommand>
    {
        public RegisterNewPalestraCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}