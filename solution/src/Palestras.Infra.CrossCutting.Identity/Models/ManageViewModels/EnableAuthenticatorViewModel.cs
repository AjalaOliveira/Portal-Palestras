using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Palestras.Infra.CrossCutting.Identity.Models.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "O {0} deve ter no mínimo {2} e no máximo {1} caracteres de tamanho.",
            MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código de verificação")]
        public string Code { get; set; }

        [ReadOnly(true)]
        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }
}