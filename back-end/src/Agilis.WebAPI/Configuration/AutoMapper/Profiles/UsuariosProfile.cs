using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.WebAPI.ViewModels.Pessoas;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
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
                 .ConstructUsing((usuario, context) =>
                    new UsuarioVO(
                        id: usuario.Id,
                        nome: usuario.NomeCompleto,
                        email: usuario.Email
                        )
                 );

            CreateMap<UsuarioBasicViewModel, UsuarioVO>()
                 .ConstructUsing((vm, context) =>
                    new UsuarioVO(
                        id: vm.Id,
                        nome: vm.Nome,
                        email: context.Mapper.Map<Email>(vm.Email)
                        )
                 );
        }
    }
}