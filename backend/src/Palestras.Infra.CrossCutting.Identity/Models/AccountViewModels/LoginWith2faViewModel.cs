using System.ComponentModel.DataAnnotations;

namespace Palestras.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "O {0} deve ter no mínimno {2} e no máximo {1} caracteres de tamanho.",
            MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código autenticador")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Lembre-se desta máquina")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}