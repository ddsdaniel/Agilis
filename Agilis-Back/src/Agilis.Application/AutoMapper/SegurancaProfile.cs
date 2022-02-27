using AutoMapper;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Models.Entities;
using Agilis.Infra.Seguranca.Models.ValueObjects;

namespace Agilis.Application.AutoMapper
{
    public class SegurancaProfile : Profile
    {
        public SegurancaProfile()
        {
            CreateMap<Usuario, UsuarioConsultaViewModel>();

            CreateMap<RefreshToken, RefreshTokenViewModel>();

            CreateMap<RefreshTokenViewModel, RefreshToken>()
              .ConstructUsing(vm => new RefreshToken(vm.Token));

            CreateMap<Senha, string>()
              .ConstructUsing(s => s.Conteudo);

            CreateMap<string, Senha>()
              .ConstructUsing(senha => new Senha(senha));

            CreateMap<UsuarioCadastroViewModel, Usuario>()
              .ConstructUsing((vm, context) => new Usuario(
                  vm.Nome,
                  vm.Sobrenome,
                  context.Mapper.Map<Senha>(vm.Senha),
                  context.Mapper.Map<Email>(vm.Email),
                  vm.Ativo,
                  vm.LicencaCompleta,
                  vm.Regra
                  ));

            CreateMap<AlterarMinhaSenhaViewModel, AlterarMinhaSenha>()
              .ConstructUsing((vm, context) => new AlterarMinhaSenha(
                  context.Mapper.Map<Senha>(vm.SenhaAtual),
                  context.Mapper.Map<Senha>(vm.NovaSenha),
                  context.Mapper.Map<Senha>(vm.ConfirmaSenha)
                  ));

            CreateMap<RedefinicaoSenhaViewModel, RedefinicaoSenha>()
              .ConstructUsing((vm, context) => new RedefinicaoSenha(
                  context.Mapper.Map<Senha>(vm.NovaSenha),
                  context.Mapper.Map<Senha>(vm.ConfirmaSenha)
                  ));
        }
    }
}
