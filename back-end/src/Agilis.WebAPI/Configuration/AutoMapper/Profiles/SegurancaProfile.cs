using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;
using Agilis.WebAPI.ViewModels.Seguranca;
using AutoMapper;
using DDS.Domain.Core.Models.ValueObjects;
using DDS.Domain.Core.Models.ValueObjects.Seguranca.Senhas;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    public class SegurancaProfile : Profile
    {
        public SegurancaProfile()
        {
            CreateMap<LoginViewModel, Login>()
                .ConstructUsing((vm, context) =>
                    new Login(
                        email: new Email(vm.Email),
                        senha: new SenhaMedia(vm.Senha, Usuario.TAMANHO_MINIMO_SENHA)
                        )
                 );

            //Senhas
            CreateMap<SenhaFraca, string>()
             .ConvertUsing(senha => senha.Conteudo);

            CreateMap<string, SenhaFraca>()
                .ConstructUsing(senha => new SenhaFraca(senha, Usuario.TAMANHO_MINIMO_SENHA));

            CreateMap<SenhaMedia, string>()
              .ConvertUsing(senha => senha.Conteudo);

            CreateMap<string, SenhaMedia>()
                .ConstructUsing(senha => new SenhaMedia(senha, Usuario.TAMANHO_MINIMO_SENHA));
        }
    }
}
