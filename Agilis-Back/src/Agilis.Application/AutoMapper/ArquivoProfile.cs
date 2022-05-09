using Agilis.Application.ViewModels;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class ArquivoProfile : Profile
    {
        public ArquivoProfile()
        {
            CreateMap<Arquivo, ArquivoViewModel>()
                .ReverseMap();
        }
    }
}
