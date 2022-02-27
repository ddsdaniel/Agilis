using Flunt.Notifications;
using MediatR;
using System;

namespace Agilis.Core.Domain.Abstractions.Events
{
    public abstract class DomainEvent : Notifiable, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
