using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
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
                        produtos: vm.Produtos
                        )
                 );

            //Ator            
            CreateMap<Ator, AtorViewModel>();

            CreateMap<AtorViewModel, Ator>()
                 .ConstructUsing((vm, context) =>
                    new Ator(
                        nome: vm.Nome,
                        produtoId: vm.ProdutoId
                        )
                 );

            //E-mail
            CreateMap<Email, string>()
               .ConvertUsing(c => c.Endereco);

            CreateMap<string, Email>()
                .ConstructUsing(email => new Email(email));

            CreateMap<Email, EmailViewModel>();

            CreateMap<string, EmailViewModel>()
                .ConstructUsing(emailString => new EmailViewModel { Endereco = emailString });

            CreateMap<EmailViewModel, Email>()
                .ConstructUsing(vm => new Email(vm.Endereco));
        }
    }
}
