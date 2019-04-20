using System.ComponentModel.DataAnnotations;

namespace Palestras.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Código de recuperação")]
        public string RecoveryCode { get; set; }
    }
}