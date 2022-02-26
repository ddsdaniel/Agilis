using Agilis.Domain.Abstractions.Entities;
using Agilis.WebAPI.ViewModels;
using AutoMapper;
using DDS.Domain.Core.Models.ValueObjects;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    public class OutrosProfile : Profile
    {
        public OutrosProfile()
        {            
            CreateMap<IntervaloDatasViewModel, IntervaloDatas>()
                  .ConstructUsing((vm, context) =>
                    new IntervaloDatas(
                        dataInicial: vm.DataInicial,
                        dataFinal: vm.DataFinal
                        )
                 );

            CreateMap<IntervaloDatas, IntervaloDatasViewModel>();

            CreateMap<EntidadeNodo, EntidadeNodoViewModel>();
        }
    }
}