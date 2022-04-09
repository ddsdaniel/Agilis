using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Dispositivo : Entidade
    {
        public string Token { get; private set; }

        protected Dispositivo()  { }

        public Dispositivo(string token)

        {
            Token = token;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Token))
                Criticar("Token do dispositivo não deve ser vazio ou nulo");
        }

        public override string ToString() => Token;
    }
}
