using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;
using System.Linq;

namespace Agilis.Application.Services.Tarefas
{
    public class TarefaCrudAppService
        : CrudAppService<TarefaViewModel, TarefaViewModel, Tarefa>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TarefaCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Tarefa>(), unitOfWork, mediator)
        {
            _unitOfWork = unitOfWork;
        }

        public string[] ConsultarTags()
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();

            var tags = tarefaRepository
                .Consultar()
                .SelectMany(t => t.Tags)
                .OrderBy(t => t.Nome)
                .Select(t => t.Nome)
                .ToArray();

            tags = tags.Distinct().ToArray();

            return tags;
        }
    }
}
