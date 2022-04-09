using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Agilis.WebAPI.HostedService
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MigrationHostedService> _logger;

        public MigrationHostedService(IServiceProvider serviceProvider, ILogger<MigrationHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciou migrations");

            using var scope = _serviceProvider.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            //TODO: await new MigrationPoupanca(unitOfWork, _logger).Migrar();

            _logger.LogInformation("Terminou migrations");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
