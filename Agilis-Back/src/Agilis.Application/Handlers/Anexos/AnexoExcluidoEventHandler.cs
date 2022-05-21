using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Events;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Agilis.Application.Handlers.Anexos
{
    public class AnexoExcluidoEventHandler : INotificationHandler<EntidadeExcluidaDomainEvent<Anexo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnexoExcluidoEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EntidadeExcluidaDomainEvent<Anexo> notification, CancellationToken cancellationToken)
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();

            var anexoExcluido = notification.Entidade;

            var tarefas = tarefaRepository
                .Consultar()
                .Where(t => t.Anexos.Any(a => a.AnexoId == anexoExcluido.Id));

            foreach (var tarefa in tarefas)
            {
                tarefa.RemoverAnexo(anexoExcluido.Id);
                await tarefaRepository.AlterarAsync(tarefa);
            }
        }
    }
}
