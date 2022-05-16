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
    public class LimpezaArquivosNaoAnexadosWorker : Worker
    {
        public LimpezaArquivosNaoAnexadosWorker(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task WorkAsync()
        {
            using var scope = ServiceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var arquivoRepository = unitOfWork.ObterRepository<Arquivo>();
            var tarefaRepository = unitOfWork.ObterRepository<Tarefa>();

            var anexosId = tarefaRepository
                .Consultar()
                .SelectMany(t => t.Anexos)
                .Select(a => a.ArquivoId)
                .ToList();

            await arquivoRepository.ExcluirNotInAsync(anexosId);
            await unitOfWork.CommitAsync();
        }
    }
}
