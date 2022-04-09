using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Extensions;

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

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsValid(Cor, nameof(Cor))
                );
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

