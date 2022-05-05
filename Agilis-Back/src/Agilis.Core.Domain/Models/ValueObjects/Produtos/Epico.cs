using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.ValueObjects.Produtos
{
    public class Epico : ValueObject<Epico>
    {
        public string Nome { get; private set; }
        public IEnumerable<Feature> Features { get; private set; }

        protected Epico() { }

        public Epico(string nome, IEnumerable<Feature> features)
        {
            Nome = nome;
            Features = features;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");
        }
        public override string ToString() => Nome;
    }
}
