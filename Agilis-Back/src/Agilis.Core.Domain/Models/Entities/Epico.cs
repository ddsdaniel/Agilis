using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Epico : Entidade
    {
        public string Nome { get; private set; }
        public Produto Produto { get; private set; }
        public IEnumerable<Feature> Features { get; private set; }

        protected Epico() { }

        public Epico(string nome, Produto produto, IEnumerable<Feature> features)
        {
            Nome = nome;
            Produto = produto;
            Features = features;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (Produto == null)
                Criticar("Produto não deve ser nulo.");

            ImportarCriticas(Produto);
        }
        public override string ToString() => Nome;
    }
}
