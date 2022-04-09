using Flunt.Notifications;
using Agilis.Core.Domain.Abstractions.Events;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Abstractions.Models.ValueObjects
{
    public abstract class EventContainer: Notifiable
    {
        public IEnumerable<DomainEvent> Eventos { get; private set; } = new List<DomainEvent>();

        public void AdicionarEvento(DomainEvent domainEvent)
        {
            if (domainEvent.Valid)
            {
                var lista = Eventos.ToList();
                lista.Add(domainEvent);
                Eventos = lista;
            }
            else
            {
                AddNotifications(domainEvent);
            }
        }
    }
}
