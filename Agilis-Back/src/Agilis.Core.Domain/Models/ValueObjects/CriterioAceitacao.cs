using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class CriterioAceitacao : ValueObject<CriterioAceitacao>
    {
        public string Nome { get; private set; }
        public int Ordem { get; private set; }

        protected CriterioAceitacao() { }

        public CriterioAceitacao(string nome, int ordem)
        {
            Nome = nome;
            Ordem = ordem;

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

