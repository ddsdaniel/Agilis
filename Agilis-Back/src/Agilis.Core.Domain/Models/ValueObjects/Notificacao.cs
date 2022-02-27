using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Extensions;
using Agilis.Core.Domain.Models.Entities;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Notificacao : ValueObject<Notificacao>
    {
        public string Titulo { get; private set; }
        public string Corpo { get; private set; }
        public IEnumerable<Dispositivo> Dispositivos { get; private set; }
        public string Icone { get; private set; }
        public string ClickAction { get; private set; }

        protected Notificacao() { }

        public Notificacao(string titulo, string corpo, IEnumerable<Dispositivo> dispositivos, string icone = null, string clickAction = null)
        {
            Titulo = titulo;
            Corpo = corpo;
            Icone = icone;
            ClickAction = clickAction;
            Dispositivos = dispositivos;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Titulo, nameof(Titulo), "Título da notificação não deve ser nulo ou vazio")
                .IsNotNullOrEmpty(Corpo, nameof(Corpo), "Corpo da notificação não deve ser nulo ou vazio")
                .IsValidArray(Dispositivos, nameof(Dispositivos))
                );
        }

        public override string ToString() => Titulo;
    }
}
