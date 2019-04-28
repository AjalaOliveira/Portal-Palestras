using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Palestras.Application.ViewModels
{
    public class PalestraPalestranteViewModel
    {
        public PalestraPalestranteViewModel() { }

        public PalestraPalestranteViewModel(PalestraViewModel palestraViewModel)
        {
            Id = palestraViewModel.Id;
            Titulo = palestraViewModel.Titulo;
            Descricao = palestraViewModel.Descricao;
            Data = palestraViewModel.Data;
            PalestranteId = palestraViewModel.PalestranteId;
        }

        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        [JsonIgnore]
        public Guid PalestranteId { get; set; }
        public PalestranteViewModel Palestrante { get; set; }
        
    }
}
