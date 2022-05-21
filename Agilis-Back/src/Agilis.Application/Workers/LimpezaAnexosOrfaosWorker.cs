using Agilis.Application.Abstractions.Workers;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Workers
{
    public class LimpezaAnexosOrfaosWorker : Worker
    {
        public LimpezaAnexosOrfaosWorker(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task WorkAsync()
        {
            using var scope = ServiceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var anexoRepository = unitOfWork.ObterRepository<Anexo>();
            var tarefaRepository = unitOfWork.ObterRepository<Tarefa>();

            var anexosId = tarefaRepository
                .Consultar()
                .SelectMany(t => t.Anexos)
                .Select(a => a.AnexoId)
                .ToList();

            await anexoRepository.ExcluirNotInAsync(anexosId);
            await unitOfWork.CommitAsync();
        }
    }
}
