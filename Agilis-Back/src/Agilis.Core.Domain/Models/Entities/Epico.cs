using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Epico : Entidade
    {
        public string Nome { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public IEnumerable<Feature> Features { get; private set; }

        protected Epico() { }

        public Epico(string nome, Guid produtoId, Produto produto, IEnumerable<Feature> features)
        {
            Nome = nome;
            ProdutoId = produtoId;
            Produto = produto;
            Features = features;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (ProdutoId == Guid.Empty)
                Criticar("Produto ID inválido.");
        }
        public override string ToString() => Nome;
    }
}
