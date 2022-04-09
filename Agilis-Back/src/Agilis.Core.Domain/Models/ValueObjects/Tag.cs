using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Tag : ValueObject<Tag>
    {
        public string Nome { get; private set; }
        public HtmlColor Cor { get; private set; }

        protected Tag() { }

        public Tag(string nome, HtmlColor cor)
        {
            Nome = nome;
            Cor = cor;

            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");

            ImportarCriticas(Cor);
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

