﻿using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
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
                        numero: vm.Numero,
                        periodo: context.Mapper.Map<IntervaloDatas>(vm.Periodo),
                        release: context.Mapper.Map<ReleaseVO>(vm.Release)
                        )
                 );

            CreateMap<SprintVO, SprintBasicViewModel>();

            CreateMap<SprintBasicViewModel, SprintVO>()
                 .ConstructUsing((vm, context) =>
                    new SprintVO(
                        id: vm.Id,
                        nome: vm.Nome,
                        numero: vm.Numero
                        )
                 );

            //Produto            
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<ProdutoViewModel, Produto>()
                 .ConstructUsing((vm, context) =>
                    new Produto(
                        id: vm.Id,
                        nome: vm.Nome,
                        requisitosNaoFuncionais: new List<RequisitoNaoFuncional>(),
                        linguagemUbiqua: new LinguagemUbiqua(new List<JargaoDoNegocio>())
                        )
                 );

            CreateMap<Release, ReleaseViewModel>();

            CreateMap<ReleaseViewModel, Release>()
                 .ConstructUsing((vm, context) =>
                    new Release(
                        ordem: vm.Ordem,
                        nome: vm.Nome,
                        time: context.Mapper.Map<TimeVO>(vm.Time),
                        sprints: new List<SprintVO>()
                        )
                 );

            CreateMap<ReleaseVO, ReleaseBasicViewModel>();

            CreateMap<ReleaseBasicViewModel, ReleaseVO>()
                 .ConstructUsing((vm, context) =>
                    new ReleaseVO(
                        ordem: vm.Ordem,
                        id: vm.Id,
                        nome: vm.Nome
                        )
                 );
        }
    }
}
