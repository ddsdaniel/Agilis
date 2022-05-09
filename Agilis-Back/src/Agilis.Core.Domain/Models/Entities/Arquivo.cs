using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Arquivo : Entidade
    {
        public string Base64 { get; private set; }

        protected Arquivo() { }

        public Arquivo(string base64)
        {
            Base64 = base64;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrWhiteSpace(Base64))
                Criticar("Base64 inválido");
        }

        public override string ToString() => Id.ToString();
    }
}
