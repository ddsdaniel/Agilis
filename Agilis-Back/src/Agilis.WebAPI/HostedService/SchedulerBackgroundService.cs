﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Agilis.Application.Abstractions.Workers;
using Agilis.Application.Workers;

namespace Agilis.WebAPI.HostedService
{
    public class SchedulerBackgroundService : BackgroundService
    {
        private readonly ILogger<SchedulerBackgroundService> _logger;
        private readonly LimpezaArquivosNaoAnexadosWorker _limpezaArquivosNaoAnexadosWorker;

        public SchedulerBackgroundService(
            ILogger<SchedulerBackgroundService> logger,
            LimpezaArquivosNaoAnexadosWorker limpezaArquivosNaoAnexadosWorker
            )
        {
            _logger = logger;
            _limpezaArquivosNaoAnexadosWorker = limpezaArquivosNaoAnexadosWorker;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ExecutarDiariamente(0, 0, _limpezaArquivosNaoAnexadosWorker, stoppingToken);
            return Task.CompletedTask;
        }

        private void ExecutarDiariamente(int horas, int minutos, Worker worker, CancellationToken stoppingToken)
        {
            var primeiraExecucao = DateTime
                .Today
                .AddHours(horas)
                .AddMinutes(minutos);

            if (primeiraExecucao < DateTime.Now)
                primeiraExecucao = primeiraExecucao.AddDays(1);

            var tempoEsperaPrimeiraExecucao = primeiraExecucao - DateTime.Now;

            Observable.Concat(
                Observable.Timer(tempoEsperaPrimeiraExecucao),
                Observable.Interval(TimeSpan.FromDays(1))
                ).Subscribe(_ =>
                {
                    _logger.LogInformation($"Executando {worker.GetType().Name}...");
                    worker.WorkAsync().Wait();
                }, stoppingToken);
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando {nameof(SchedulerBackgroundService)}...");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Parando {nameof(SchedulerBackgroundService)}...");
            return base.StopAsync(cancellationToken);
        }

    }
}
