using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Handlers
{
    public class DomainEventHandlerException : Exception
    {
        private readonly List<Notification> _notifications;

        public string NomeHandler { get; private set; }
        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        public DomainEventHandlerException(string nomeHandler, params Notification[] notifications)
        {
            NomeHandler = nomeHandler;
            _notifications = notifications?.ToList();
        }
        public DomainEventHandlerException(string nomeHandler, string property, string message)
        {
            Notification notification
                = new Notification(property, message);

            var notifications = new List<Notification> { notification };

            NomeHandler = nomeHandler;
            _notifications = notifications;
        }
    
        public DomainEventHandlerException(Type handler, string property, string message)
        {
            Notification notification
                = new Notification(property, message);

            var notifications = new List<Notification> { notification };

            NomeHandler = handler.Name;
            _notifications = notifications;
        }
    }
}
