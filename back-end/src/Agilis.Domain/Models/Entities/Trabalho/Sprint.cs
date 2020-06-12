using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Sprint : Entity
    {
        public int Numero { get; private set; }
        public IntervaloDatas Periodo { get; private set; }
        public string Nome { get; private set; }
        public ReleaseVO Release { get; private set; }

        protected Sprint()
        {

        }

        public Sprint(string nome, int numero, IntervaloDatas periodo, ReleaseVO release)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser vazio ou nulo")
                .IsGreaterOrEqualsThan(numero, 0, nameof(Numero), "Número deve ser maior ou igual a zero")
                .IsNotNull(periodo, nameof(Periodo), "Período não deve ser nulo")
                .IfNotNull(periodo, c => c.Join(periodo))
                .IsNotNull(release, nameof(Release), "Release não deve ser nulo")
                .IfNotNull(release, c => c.Join(release))                
                );

            Nome = nome;
            Numero = numero;
            Periodo = periodo;
            Release = release;
        }

        public override string ToString() => Nome;
    }
}
