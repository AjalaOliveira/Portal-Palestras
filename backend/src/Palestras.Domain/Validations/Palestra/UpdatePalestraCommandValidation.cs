using Palestras.Domain.Commands.Palestra;

namespace Palestras.Domain.Validations.Palestra
{
    public class UpdatePalestraCommandValidation : PalestraValidation<UpdatePalestraCommand>
    {
        public UpdatePalestraCommandValidation()
        {
            ValidateId();
            ValidaTitulo();
            ValidateData();
            ValidateDescricao();
            ValidateIdPalestrante();
        }
    }
}