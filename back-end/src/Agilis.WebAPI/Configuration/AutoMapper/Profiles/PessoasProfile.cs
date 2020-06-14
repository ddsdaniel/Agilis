using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Agilis.WebAPI.ViewModels.Pessoas;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    public class PessoasProfile : Profile
    {
        public PessoasProfile()
        {
            //Times
            CreateMap<Time, TimeViewModel>();

            CreateMap<TimeViewModel, Time>()
                  .ConstructUsing((vm, context) =>
                    new Time(
                        nome: vm.Nome,
                        escopo: vm.Escopo,
                        administradores: context.Mapper.Map<UsuarioVO[]>(vm.Administradores),
                        colaboradores: context.Mapper.Map<UsuarioVO[]>(vm.Colaboradores),
                        releases: context.Mapper.Map<ReleaseVO[]>(vm.Releases),
                        produtos: context.Mapper.Map<ProdutoVO[]>(vm.Produtos)
                        )
                 );

            CreateMap<Time, TimeVO>()
                .ConstructUsing((vm, context) =>
                  new TimeVO(
                      id: vm.Id,
                      nome: vm.Nome
                      )
               );

            CreateMap<TimeVO, TimeViewModel>();

            CreateMap<TimeBasicViewModel, TimeVO>().ConstructUsing((vm, context) =>
                 new TimeVO(
                     id: vm.Id,
                     nome: vm.Nome
                     )
              );

            CreateMap<TimeVO, TimeBasicViewModel>();

            CreateMap<TimeViewModel, TimeVO>()
               .ConstructUsing((vm, context) =>
                 new TimeVO(
                     id: vm.Id,
                     nome: vm.Nome
                     )
              );

            //Ator
            CreateMap<Ator, AtorViewModel>()
                .ReverseMap();

            //E-mail
            CreateMap<Email, string>()
               .ConvertUsing(c => c.Endereco);

            CreateMap<string, Email>()
                .ConstructUsing(email => new Email(email));

            CreateMap<Email, EmailViewModel>();
        }
    }
}
