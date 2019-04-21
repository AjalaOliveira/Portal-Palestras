﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Palestras.Application.ViewModels
{
    public class PalestraViewModel
    {
        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Nascimento")]
        public DateTime BirthDate { get; set; }
    }
}