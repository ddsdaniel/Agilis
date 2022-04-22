using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Events;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Agilis.Application.Handlers.Tags
{
    public class ExcluirTagsNaoUsadasEventHandler :
        INotificationHandler<EntidadeExcluidaDomainEvent<Tarefa>>,
        INotificationHandler<EntidadeAlteradaDomainEvent<Tarefa>>,
        INotificationHandler<EntidadeAdicionadaDomainEvent<Tarefa>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcluirTagsNaoUsadasEventHandler(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EntidadeExcluidaDomainEvent<Tarefa> notification, CancellationToken cancellationToken)
        {
            await ExcluirAsync();
        }

        public async Task Handle(EntidadeAdicionadaDomainEvent<Tarefa> notification, CancellationToken cancellationToken)
        {
            await ExcluirAsync();
        }

        public async Task Handle(EntidadeAlteradaDomainEvent<Tarefa> notification, CancellationToken cancellationToken)
        {
            await ExcluirAsync();
        }

        private async Task ExcluirAsync()
        {
            //var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();
            //var tagRepository = _unitOfWork.ObterRepository<Tag>();

            //var tagsIds = tarefaRepository
            //    .Consultar()
            //    .SelectMany(t => t.Tags)
            //    .Select(t => t.Id)
            //    .Distinct();

            //await tagRepository.ExcluirAsync(t => !tagsIds.Any(id => t.Id == id));
        }
    }
}
