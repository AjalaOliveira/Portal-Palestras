using AutoMapper;
using Palestras.Application.ViewModels;
using Palestras.Domain.Models;

namespace Palestras.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Palestrante, PalestranteViewModel>();
            CreateMap<Palestra, PalestraViewModel>();
        }
    }
}