using AutoMapper;
using Palestras.Application.ViewModels;
using Palestras.Domain.Commands.Palestra;
using Palestras.Domain.Commands.Palestrante;

namespace Palestras.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PalestranteViewModel, RegisterNewPalestranteCommand>()

                .ConstructUsing(c => new RegisterNewPalestranteCommand(c.Nome, c.MiniBio, c.Url));
            CreateMap<PalestranteViewModel, UpdatePalestranteCommand>()
                .ConstructUsing(c => new UpdatePalestranteCommand(c.Id, c.Nome, c.MiniBio, c.Url));

            CreateMap<PalestraViewModel, RegisterNewPalestraCommand>()
                .ConstructUsing(c => new RegisterNewPalestraCommand(c.Titulo, c.Descricao, c.Data, c.PalestranteId));
            CreateMap<PalestraViewModel, UpdatePalestraCommand>()
                .ConstructUsing(c => new UpdatePalestraCommand(c.Id, c.Titulo, c.Descricao, c.Data, c.PalestranteId));
        }
    }
}