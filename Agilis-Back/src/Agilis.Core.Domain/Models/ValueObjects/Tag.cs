using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Tag : ValueObject<Tag>
    {
        public string Nome { get; private set; }
        
        protected Tag() { }

        public Tag(string nome)
        {
            Nome = nome;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                );
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
