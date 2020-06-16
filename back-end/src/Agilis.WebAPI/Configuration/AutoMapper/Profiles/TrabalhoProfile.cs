using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.WebAPI.ViewModels.Trabalho;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;
using System.Collections.Generic;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    public class TrabalhoProfile : Profile
    {
        public TrabalhoProfile()
        {
            CreateMap<Comentario, ComentarioViewModel>()
                .ReverseMap(); 
            
            CreateMap<Milestone, MilestoneViewModel>()
              .ReverseMap();

            CreateMap<UserStory, UserStoryViewModel>()
                .ReverseMap();

            //Sprint            
            CreateMap<Sprint, SprintViewModel>();

            CreateMap<SprintViewModel, Sprint>()
                 .ConstructUsing((vm, context) =>
                    new Sprint(
                        nome: vm.Nome,
                        periodo: context.Mapper.Map<IntervaloDatas>(vm.Periodo)
                        )
                 );

            //Produto            
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<ProdutoViewModel, Produto>()
                 .ConstructUsing((vm, context) =>
                    new Produto(
                        nome: vm.Nome,
                        requisitosNaoFuncionais: new List<RequisitoNaoFuncional>(),
                        linguagemUbiqua: new LinguagemUbiqua(new List<JargaoDoNegocio>())
                        )
                 );

            CreateMap<Release, ReleaseViewModel>();

            CreateMap<ReleaseViewModel, Release>()
                 .ConstructUsing((vm, context) =>
                    new Release(
                        nome: vm.Nome,
                        sprints: new List<SprintFK>()
                        )
                 );
        }
    }
}
