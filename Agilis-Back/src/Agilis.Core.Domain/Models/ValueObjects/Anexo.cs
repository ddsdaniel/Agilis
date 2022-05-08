using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Anexo : ValueObject<Anexo>
    {
        public string Nome { get; private set; }
        public string Base64 { get; private set; }
        public bool Imagem { get; private set; }

        protected Anexo() { }

        public Anexo(string nome, string base64, bool imagem)
        {
            Nome = nome;
            Base64 = base64;
            Imagem = imagem;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrWhiteSpace(Base64))
                Criticar("Base64 inválido");

            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");
        }

        public override string ToString() => Nome;
    }
}
