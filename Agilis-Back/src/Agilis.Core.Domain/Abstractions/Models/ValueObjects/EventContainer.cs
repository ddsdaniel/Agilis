using Agilis.Core.Domain.Abstractions.Events;
using DDS.Validacoes.Abstractions.Models;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Abstractions.Models.ValueObjects
{
    public abstract class EventContainer : Validavel
    {
        public IEnumerable<DomainEvent> Eventos { get; private set; } = new List<DomainEvent>();

        public void AdicionarEvento(DomainEvent domainEvent)
        {
            if (domainEvent.Valido)
            {
                var lista = Eventos.ToList();
                lista.Add(domainEvent);
                Eventos = lista;
            }
            else
            {
                ImportarCriticas(domainEvent);
            }
        }
    }
}
