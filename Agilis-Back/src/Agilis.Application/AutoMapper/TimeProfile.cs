using Agilis.Application.ViewModels.Times;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class TimeProfile : Profile
    {
        public TimeProfile()
        {
            CreateMap<Time, TimeViewModel>()
                .ReverseMap();
        }
    }
}
