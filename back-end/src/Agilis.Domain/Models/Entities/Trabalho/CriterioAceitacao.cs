using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class CriterioAceitacao : ValueObject<CriterioAceitacao>
    {
        public string Nome { get; private set; }

        protected CriterioAceitacao()
        {

        }

        public CriterioAceitacao(string nome)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser vazio ou nulo")
                );

            Nome = nome;
        }
    }
}
