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
                .ConstructUsing(c => new RegisterNewPalestranteCommand(c.Nome, c.Email, c.BirthDate));
            CreateMap<PalestranteViewModel, UpdatePalestranteCommand>()
                .ConstructUsing(c => new UpdatePalestranteCommand(c.Id, c.Nome, c.Email, c.BirthDate));
            CreateMap<PalestraViewModel, RegisterNewPalestraCommand>()
                .ConstructUsing(c => new RegisterNewPalestraCommand(c.Titulo, c.Email, c.BirthDate));
            CreateMap<PalestraViewModel, UpdatePalestraCommand>()
                .ConstructUsing(c => new UpdatePalestraCommand(c.Id, c.Titulo, c.Email, c.BirthDate));
        }
    }
}