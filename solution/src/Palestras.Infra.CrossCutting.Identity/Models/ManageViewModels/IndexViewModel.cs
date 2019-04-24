using System.ComponentModel.DataAnnotations;

namespace Palestras.Infra.CrossCutting.Identity.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Nome")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de telefone")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}