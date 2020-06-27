using Agilis.Domain.Abstractions.Entities.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Agilis.WebAPI.ViewModels.Trabalho;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects;
using System.Collections.Generic;

namespace Agilis.WebAPI.Configuration.AutoMapper.Profiles
{
    /// <summary>
    /// Configurações do mapeamento (auto-mapper) para as classes do namespace Trabalho
    /// </summary>
    public class TrabalhoProfile : Profile
    {

        /// <summary>
        /// Construtor responsável pelos mapeamentos
        /// </summary>
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
                        linguagemUbiqua: new LinguagemUbiqua(new List<JargaoDoNegocio>()),
                        jornadas: new List<Jornada>()
                        )
                 );

            CreateMap<Jornada, JornadaViewModel>();

            CreateMap<JornadaViewModel, Jornada>()
                 .ConstructUsing((vm, context) =>
                    new Jornada(
                        posicao: vm.Posicao,
                        nome: vm.Nome,
                        fases: context.Mapper.Map<Fase[]>(vm.Fases)
                        )
                 );

            CreateMap<ProdutoFK, ProdutoViewModel>();

            //Releases
            CreateMap<Release, ReleaseViewModel>();

            CreateMap<ReleaseViewModel, Release>()
                 .ConstructUsing((vm, context) =>
                    new Release(
                        nome: vm.Nome,
                        sprints: context.Mapper.Map<SprintFK[]>(vm.Sprints),
                        productBacklog: context.Mapper.Map<ProductBacklog>(vm.ProductBacklog)
                        )
                 );

            CreateMap<ProductBacklog, ProductBacklogViewModel>();

            CreateMap<ProductBacklogViewModel, ProductBacklog>()
                 .ConstructUsing((vm, context) =>
                    new ProductBacklog(
                        fases: context.Mapper.Map<Fase[]>(vm.Fases)
                        )
                 );

            CreateMap<Fase, FaseViewModel>();

            CreateMap<FaseViewModel, Fase>()
                 .ConstructUsing((vm, context) =>
                    new Fase(
                        posicao: vm.Posicao,
                        nome: vm.Nome,
                        tarefas: context.Mapper.Map<TarefaFK[]>(vm.Tarefas)
                        )
                 );

            CreateMap<Tarefa, TarefaViewModel>();
        }
    }
}
