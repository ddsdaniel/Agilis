using Agilis.Application.ViewModels.Produtos;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects.Produtos;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ReverseMap();

            CreateMap<Feature, FeatureViewModel>()
                .ReverseMap();
        }
    }
}
