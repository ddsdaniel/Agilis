using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Core.Domain.Models.ValueObjects.CheckLists;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaViewModel>()
                .ReverseMap();

            CreateMap<Tag, TagViewModel>()
                .ReverseMap();

            CreateMap<CheckList, CheckListViewModel>()
                .ReverseMap();

            CreateMap<ItemCheckList, ItemCheckListViewModel>()
                .ReverseMap();
        }
    }
}
