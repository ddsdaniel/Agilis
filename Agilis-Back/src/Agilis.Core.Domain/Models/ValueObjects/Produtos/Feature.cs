using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.ValueObjects.Produtos
{
    public class Feature : ValueObject<Feature>
    {
        public string Nome { get; private set; }
        
        protected Feature() { }

        public Feature(string nome)
        {
            Nome = nome;
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
