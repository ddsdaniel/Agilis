using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Events;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Agilis.Application.Handlers.Anexos
{
    public class TarefaExcluidaEventHandler : INotificationHandler<EntidadeExcluidaDomainEvent<Tarefa>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TarefaExcluidaEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EntidadeExcluidaDomainEvent<Tarefa> notification, CancellationToken cancellationToken)
        {
            var anexoRepository = _unitOfWork.ObterRepository<Anexo>();
            foreach (var anexo in notification.Entidade.Anexos)
            {
                await anexoRepository.ExcluirAsync(a => a.Id == anexo.AnexoId);
            }
        }
    }
}
