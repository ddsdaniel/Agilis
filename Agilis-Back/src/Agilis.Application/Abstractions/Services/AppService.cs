using MediatR;
using Flunt.Notifications;
using Agilis.Core.Domain.Handlers;
using Agilis.Core.Domain.Abstractions.Events;
using System.Threading.Tasks;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Application.Abstractions.Services
{
    public abstract class AppService : Notifiable
    {
        private readonly IMediator _mediator;

        protected AppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublicarDomainEventsAsync(EventContainer eventContainer)
        {
            foreach (var evento in eventContainer.Eventos)
            {
                await PublicarDomainEventAsync(evento);
            }
        }

        public async Task PublicarDomainEventAsync(DomainEvent domainEvent)
        {
            try
            {
                await _mediator.Publish(domainEvent);

                if (domainEvent.Invalid)
                    AddNotifications(domainEvent);
            }
            catch (DomainEventHandlerException exception)
            {
                AddNotifications(exception.Notifications);
            }
        }
    }
}
