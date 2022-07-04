using Agilis.Application.ViewModels.Releases;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class ReleaseProfile : Profile
    {
        public ReleaseProfile()
        {
            CreateMap<Release, ReleaseViewModel>()
                .ReverseMap();
        }
    }
}
