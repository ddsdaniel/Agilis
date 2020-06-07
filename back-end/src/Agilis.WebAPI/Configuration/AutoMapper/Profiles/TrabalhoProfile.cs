using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Agilis.WebAPI.ViewModels.Trabalho;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            //Produto            
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<ProdutoViewModel, Produto>()
                 .ConstructUsing((vm, context) =>
                    new Produto(
                        nome: vm.Nome,
                        time: context.Mapper.Map<TimeVO>(vm.Time),
                        requisitosNaoFuncionais: new List<RequisitoNaoFuncional>(),
                        linguagemUbiqua: new LinguagemUbiqua(new List<JargaoDoNegocio>())
                        )
                 );

            CreateMap<ProdutoVO, ProdutoBasicViewModel>();

            CreateMap<ProdutoBasicViewModel, ProdutoVO>()
                 .ConstructUsing((vm, context) =>
                    new ProdutoVO(
                        id: vm.Id,
                        nome: vm.Nome
                        )
                 );

            CreateMap<Release, ReleaseViewModel>();

            CreateMap<ReleaseViewModel, Release>()
                 .ConstructUsing((vm, context) =>
                    new Release(
                        nome: vm.Nome,
                        time: context.Mapper.Map<Time>(vm.Time)
                        )
                 );
        }
    }
}
