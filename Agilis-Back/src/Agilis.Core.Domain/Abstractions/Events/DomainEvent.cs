using DDS.Validacoes.Abstractions.Models;
using MediatR;
using System;

namespace Agilis.Core.Domain.Abstractions.Events
{
    public abstract class DomainEvent : Validavel, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
