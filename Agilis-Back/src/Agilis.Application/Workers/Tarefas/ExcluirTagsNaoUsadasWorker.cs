using Agilis.Application.Abstractions.Workers;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Workers.Tarefas
{
    public class ExcluirTagsNaoUsadasWorker : Worker
    {
        public ExcluirTagsNaoUsadasWorker(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task WorkAsync()
        {
            using var scope = ServiceProvider.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var tarefaRepository = unitOfWork.ObterRepository<Tarefa>();
            var tagRepository = unitOfWork.ObterRepository<Tag>();

            var tagsIds = tarefaRepository
                .Consultar()
                .SelectMany(t => t.Tags)
                .Select(t => t.Id)
                .Distinct();

            await tagRepository.ExcluirAsync(t => !tagsIds.Any(id => t.Id == id));

            await unitOfWork.CommitAsync();
        }
    }
}
