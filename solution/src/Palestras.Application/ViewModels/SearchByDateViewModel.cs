using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Palestras.Application.ViewModels
{
    public class SearchByDateViewModel
    {
        public SearchByDateViewModel()
        {

        }

        public SearchByDateViewModel(DateTime data, IEnumerable<PalestraViewModel> palestraViewModel)
        {
            Data = data;
            PalestraViewModel = palestraViewModel;
        }

        [Required(ErrorMessage = "A data é obrigatória")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data")]
        public DateTime Data { get; set; }

        public IEnumerable<PalestraViewModel> PalestraViewModel { get; set; }
    }
}
