using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Agilis.WebAPI.ViewModels.Seguranca;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;
using Agilis.WebAPI.ViewModels.Pessoas;
using Agilis.Domain.Models.ValueObjects;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;

namespace Agilis.WebAPI.Configuration
{
    /// <summary>
    /// Define o mapeamento entre as entidades e as view models, bem como alguns value objects específicos
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// Construtor que configura o mapeamento das propriedades
        /// </summary>
        public AutoMapperConfig()
        {
            //DDS.Domain.Core
            CreateMap<Email, string>()
               .ConvertUsing(c => c.Endereco);

            CreateMap<string, Email>()
                .ConstructUsing(email => new Email(email));

            //Senhas
            CreateMap<SenhaFraca, string>()
             .ConvertUsing(senha => senha.Conteudo);

            CreateMap<string, SenhaFraca>()
                .ConstructUsing(senha => new SenhaFraca(senha, Usuario.TAMANHO_MINIMO_SENHA));

            CreateMap<SenhaMedia, string>()
              .ConvertUsing(senha => senha.Conteudo);

            CreateMap<string, SenhaMedia>()
                .ConstructUsing(senha => new SenhaMedia(senha, Usuario.TAMANHO_MINIMO_SENHA));

            //Trabalho
            CreateMap<Comentario, ComentarioViewModel>()
                .ReverseMap();

            CreateMap<Milestone, MilestoneViewModel>()
                .ReverseMap();

            CreateMap<UserStory, UserStoryViewModel>()
                .ReverseMap();

            //Pessoas            
            CreateMap<Ator, AtorViewModel>()
                .ReverseMap();

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

            //Seguranca
            CreateMap<LoginViewModel, Login>();
        }
    }
}