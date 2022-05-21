using Agilis.Application.ViewModels;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class AnexoProfile : Profile
    {
        public AnexoProfile()
        {
            CreateMap<Anexo, AnexoViewModel>()
                .ReverseMap();
        }
    }
}
