using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
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
                        administradores: context.Mapper.Map<UsuarioFK[]>(vm.Administradores),
                        colaboradores: context.Mapper.Map<UsuarioFK[]>(vm.Colaboradores),
                        releases: context.Mapper.Map<ReleaseFK[]>(vm.Releases),
                        produtos: context.Mapper.Map<ProdutoFK[]>(vm.Produtos)
                        )
                 );

            CreateMap<Time, TimeFK>()
                .ConstructUsing((vm, context) =>
                  new TimeFK(
                      id: vm.Id,
                      nome: vm.Nome
                      )
               );

            CreateMap<TimeFK, TimeViewModel>();

            CreateMap<TimeViewModel, TimeFK>()
               .ConstructUsing((vm, context) =>
                 new TimeFK(
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

            CreateMap<EmailViewModel, Email>()
                .ConstructUsing(vm => new Email(vm.Endereco));
        }
    }
}
