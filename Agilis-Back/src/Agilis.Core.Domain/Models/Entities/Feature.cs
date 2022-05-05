using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Models.Entities;

namespace Agilis.Core.Domain.Models.ValueObjects.Produtos
{
    public class Feature : Entidade
    {
        public string Nome { get; private set; }
        public Produto Produto { get; private set; }

        protected Feature() { }

        public Feature(string nome, Produto produto)
        {
            Nome = nome;
            Produto = produto;
            Validar();
        }


        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (Produto == null)
                Criticar("Produto não deve ser nulo");

            ImportarCriticas(Produto);
        }

        public override string ToString() => $"{Nome} - {Produto.Nome}";
    }
}
