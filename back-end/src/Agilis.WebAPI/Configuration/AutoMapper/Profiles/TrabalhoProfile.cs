﻿using Agilis.Domain.Abstractions.Entities.Trabalho;
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

            CreateMap<CriterioAceitacao, CriterioAceitacaoViewModel>();

            CreateMap<CriterioAceitacaoViewModel, CriterioAceitacao>()
                 .ConstructUsing((vm, context) =>
                    new CriterioAceitacao(
                        nome: vm.Nome
                        )
                 );

            CreateMap<UserStory, UserStoryViewModel>();

            CreateMap<UserStoryViewModel, UserStory>()
                 .ConstructUsing((vm, context) =>
                    new UserStory(
                        nome: vm.Nome,
                        ator: vm.Ator,
                        narrativa: vm.Narrativa,
                        objetivo: vm.Objetivo,
                        epicoId: vm.EpicoId,
                        criteriosAceitacao: context.Mapper.Map<CriterioAceitacao[]>(vm.CriteriosAceitacao)
                        )
                 );

            //Sprint            
            CreateMap<Sprint, SprintViewModel>();

            CreateMap<SprintViewModel, Sprint>()
                 .ConstructUsing((vm, context) =>
                    new Sprint(
                        nome: vm.Nome,
                        periodo: context.Mapper.Map<IntervaloDatas>(vm.Periodo)
                        )
                 );

            //Epico            
            CreateMap<Epico, EpicoViewModel>();

            CreateMap<EpicoViewModel, Epico>()
                 .ConstructUsing((vm, context) =>
                    new Epico(
                        nome: vm.Nome,
                        temaId: vm.TemaId
                        )
                 );

            //Tema            
            CreateMap<Tema, TemaViewModel>();

            CreateMap<TemaViewModel, Tema>()
                 .ConstructUsing((vm, context) =>
                    new Tema(
                        nome: vm.Nome,
                        produtoId: vm.ProdutoId
                        )
                 );

            //Produto            
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<ProdutoViewModel, Produto>()
                 .ConstructUsing((vm, context) =>
                    new Produto(
                        nome: vm.Nome,
                        timeId: vm.TimeId
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
