using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.Entities.Tags
{
    public class Tag : Entidade
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
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");

            ImportarCriticas(Cor);
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

