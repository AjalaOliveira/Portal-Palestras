using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Palestras.Application.ViewModels
{
    public class PalestraViewModel
    {
        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "O titulo é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data")]
        public DateTime Data { get; set; }

        [DisplayName("Palestrante")]        
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "O palestrante é obrigatório")]
        public Guid PalestranteId { get; set; }
    }
}