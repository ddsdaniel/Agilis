using Agilis.Application.ViewModels.Releases;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class ReleaseProfile : Profile
    {
        public ReleaseProfile()
        {
            CreateMap<Release, ReleaseViewModel>()
                .ReverseMap();

            CreateMap<Versao, string>()
              .ConstructUsing(versao => versao.ToString());

            CreateMap<string, Versao>()
              .ConstructUsing(versao => new Versao(versao));
        }
    }
}
