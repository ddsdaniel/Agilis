using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Sprint : Entity
    {
        public IntervaloDatas Periodo { get; private set; }
        public string Nome { get; private set; }

        protected Sprint()
        {

        }

        public Sprint(string nome)
            : this(nome, new IntervaloDatas(null, null))
        {
        }

        public Sprint(string nome, IntervaloDatas periodo)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser vazio ou nulo")
                .IsNotNull(periodo, nameof(Periodo), "Período não deve ser nulo")
                .IfNotNull(periodo, c => c.Join(periodo))
                );

            Nome = nome;
            Periodo = periodo;
        }

        public override string ToString() => Nome;
    }
}
