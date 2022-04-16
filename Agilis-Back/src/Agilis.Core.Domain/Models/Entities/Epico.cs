using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;

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
            //TODO: validar
        }

        public override string ToString() => Nome;
    }
}
