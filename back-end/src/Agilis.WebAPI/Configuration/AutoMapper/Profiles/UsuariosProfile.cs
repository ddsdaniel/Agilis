using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
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

            CreateMap<Usuario, UsuarioFK>()
                 .ConstructUsing((usuario, context) =>
                    new UsuarioFK(
                        id: usuario.Id,
                        nome: usuario.NomeCompleto,
                        email: usuario.Email.Endereco
                        )
                 );
        }
    }
}