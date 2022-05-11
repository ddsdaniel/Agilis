using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Events;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Agilis.Application.Handlers.Arquivos
{
    public class ArquivoExcluidoEventHandler : INotificationHandler<EntidadeExcluidaDomainEvent<Arquivo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArquivoExcluidoEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EntidadeExcluidaDomainEvent<Arquivo> notification, CancellationToken cancellationToken)
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();

            var arquivoExcluido = notification.Entidade;

            var tarefas = tarefaRepository
                .Consultar()
                .Where(t => t.Anexos.Any(a => a.ArquivoId == arquivoExcluido.Id));

            foreach (var tarefa in tarefas)
            {
                tarefa.RemoverAnexo(arquivoExcluido.Id);
                await tarefaRepository.AlterarAsync(tarefa);
            }
        }
    }
}
