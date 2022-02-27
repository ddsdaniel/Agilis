using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Abstractions.Factories;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Events;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Agilis.Application.Handlers.Notificacoes
{
    public class UsuarioAdicionadoDomainEvent : INotificationHandler<EntidadeAdicionadaDomainEvent<Usuario>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationService _notificationService;

        public UsuarioAdicionadoDomainEvent(IServiceProvider serviceProvider, INotificationService notificationService)
        {
            _serviceProvider = serviceProvider;
            _notificationService = notificationService;
        }

        public async Task Handle(EntidadeAdicionadaDomainEvent<Usuario> notification, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var unitOfWorkFactory = scope.ServiceProvider.GetService<IUnitOfWorkFactory>();

            var unitOfWorkCatalogo = unitOfWorkFactory.ObterUnitOfWorkCatalogo();

            var usuarioRepository = unitOfWorkCatalogo.ObterRepository<Usuario>();

            var administradores = usuarioRepository
                .Consultar()
                .Where(u => u.Ativo && u.Regra == RegraUsuario.Admin)
                .ToList();

            var dispositivos = new List<Dispositivo>();
            foreach (var admin in administradores)
            {
                var unitOfWorkInquilino = unitOfWorkFactory.ObterUnitOfWorkInquilino(admin.Email);
                var dispositivoRepository = unitOfWorkInquilino.ObterRepository<Dispositivo>();
                dispositivos.AddRange(dispositivoRepository.Consultar());
            }

            if (dispositivos.Any())
            {
                var notificacao = new Notificacao(
                    titulo: "Novo usuário",
                    dispositivos: dispositivos,
                    corpo: $"{notification.Entidade.Email} cadastrou-se no Agilis",
                    icone: "/assets/images/Agilis-192.png",
                    clickAction: "/#/transacoes"
                    );

                await _notificationService.NotificarAsync(notificacao);
            }
        }
    }
}
