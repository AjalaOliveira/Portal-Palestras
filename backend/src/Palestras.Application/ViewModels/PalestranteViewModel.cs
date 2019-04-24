using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Palestras.Application.ViewModels
{
    public class PalestranteViewModel
    {        
        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Mini Bio é obrigatória")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Mini Bio")]
        public string MiniBio { get; set; }

        [Required(ErrorMessage = "A URL é obrigatória")]
        [Url(ErrorMessage = "Informe uma URL válida!")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("URL")]
        public string Url { get; set; }

        [JsonIgnore]
        public virtual ICollection<PalestraViewModel> Palestras { get; set; }
    }
}