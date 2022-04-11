using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Tarefas;

namespace Agilis.Application.Services.Tarefas
{
    public class TarefaCrudAppService
        : CrudAppService<TarefaViewModel, TarefaViewModel, Tarefa>
    {
        public TarefaCrudAppService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(mapper, unitOfWork.ObterRepository<Tarefa>(), unitOfWork)
        {
        }
    }
}
