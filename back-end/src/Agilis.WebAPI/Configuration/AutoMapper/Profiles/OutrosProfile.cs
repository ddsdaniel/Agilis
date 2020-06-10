using Agilis.WebAPI.ViewModels;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    public class OutrosProfile : Profile
    {
        public OutrosProfile()
        {
            //Times
            CreateMap<IntervaloDatasViewModel, IntervaloDatas>()
                  .ConstructUsing((vm, context) =>
                    new IntervaloDatas(
                        dataInicial: vm.DataInicial,
                        dataFinal: vm.DataFinal
                        )
                 );
        }
    }
}