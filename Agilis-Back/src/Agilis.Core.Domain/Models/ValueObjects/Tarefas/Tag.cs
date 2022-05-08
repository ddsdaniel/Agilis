using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.ValueObjects.Tarefas
{
    public class Tag : ValueObject<Tag>
    {
        public string Nome { get; private set; }

        protected Tag() { }

        public Tag(string nome)
        {
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

