using Agilis.Application.ViewModels;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Tarefas;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaViewModel>()
                .ReverseMap();

            CreateMap<CheckList, CheckListViewModel>()
                .ReverseMap();

            CreateMap<ItemCheckList, ItemCheckListViewModel>()
                .ReverseMap();

            CreateMap<Comentario, ComentarioViewModel>()
                .ReverseMap();

            CreateMap<Anexo, AnexoViewModel>()
                .ReverseMap();
        }
    }
}
