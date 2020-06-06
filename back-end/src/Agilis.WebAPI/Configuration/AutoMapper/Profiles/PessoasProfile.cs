using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Agilis.WebAPI.ViewModels.Pessoas;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;

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
                        produtos: context.Mapper.Map<ProdutoVO[]>(vm.Produtos)
                        )
                 );

            //Ator
            CreateMap<Ator, AtorViewModel>()
                .ReverseMap();

            //Usuario
            CreateMap<Usuario, UsuarioConsultaViewModel>();

            CreateMap<UsuarioCadastroViewModel, Usuario>()
                 .ConstructUsing((vm, context) =>
                    new Usuario(
                        email: new Email(vm.Email),
                        nome: vm.Nome,
                        sobrenome: vm.Sobrenome,
                        senha: new SenhaMedia(vm.Senha, Usuario.TAMANHO_MINIMO_SENHA),
                        regra: vm.Regra
                        )
                 );

            CreateMap<UsuarioVO, UsuarioBasicViewModel>();

            CreateMap<Usuario, UsuarioVO>()
                 .ConstructUsing((entity, context) =>
                    new UsuarioVO(
                        id: entity.Id,
                        nome: entity.NomeCompleto
                        )
                 );

            CreateMap<UsuarioBasicViewModel, UsuarioVO>()
                 .ConstructUsing((vm, context) =>
                    new UsuarioVO(
                        id: vm.Id,
                        nome: vm.Nome
                        )
                 );

            //E-mail
            CreateMap<Email, string>()
               .ConvertUsing(c => c.Endereco);

            CreateMap<string, Email>()
                .ConstructUsing(email => new Email(email));
        }
    }
}
