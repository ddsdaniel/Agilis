using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;

namespace Agilis.Application.Services.Tarefas
{
    public class TarefaCrudAppService
        : CrudAppService<TarefaViewModel, TarefaViewModel, Tarefa>
    {
        public TarefaCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Tarefa>(), unitOfWork, mediator)
        {
        }
    }
}
