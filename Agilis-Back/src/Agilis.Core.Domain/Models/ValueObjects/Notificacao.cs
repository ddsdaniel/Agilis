using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Models.Entities;
using System;
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
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Titulo))
                Criticar("Título da notificação não deve ser nulo ou vazio");

            if (String.IsNullOrEmpty(Corpo))
                Criticar("Corpo da notificação não deve ser nulo ou vazio");

            ImportarCriticas(Dispositivos);
        }

        public override string ToString() => Titulo;
    }
}
