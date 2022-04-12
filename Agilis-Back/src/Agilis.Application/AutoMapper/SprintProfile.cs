using Agilis.Application.ViewModels.Sprints;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class SprintProfile : Profile
    {
        public SprintProfile()
        {
            CreateMap<Sprint, SprintViewModel>()
                .ReverseMap();
        }
    }
}
