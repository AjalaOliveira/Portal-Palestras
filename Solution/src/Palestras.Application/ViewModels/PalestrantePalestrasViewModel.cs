using System;
using System.Collections.Generic;
using System.Text;

namespace Palestras.Application.ViewModels
{
    public class PalestrantePalestrasViewModel
    {
        public PalestrantePalestrasViewModel() { }

        public PalestrantePalestrasViewModel (PalestranteViewModel palestranteViewModel)
        {
            Id = palestranteViewModel.Id;
            Nome = palestranteViewModel.Nome;
            MiniBio = palestranteViewModel.MiniBio;
            Url = palestranteViewModel.Url;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string MiniBio { get; set; }
        public string Url { get; set; }
        public IEnumerable<PalestraViewModel> Palestras { get; set; }
    }
}
